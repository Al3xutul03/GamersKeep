using BusinessLogic.Options;
using BusinessLogic.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repository.Entity;
using Repository.Enums.Types;
using Repository.Repositories.Abstractions;
using Stripe;
using Stripe.Checkout;

namespace BusinessLogic.Services;

/// <summary>
/// Service managing the processing, signature validation, and idempotency of incoming Stripe webhooks.
/// </summary>
public class StripeWebhookService(
    IOrderRepository orderRepository,
    ICartService cartService,
    IStockService stockService,
    IProcessedStripeEventRepository processedEventRepository,
    IOptions<StripeOptions> options,
    ILogger<StripeWebhookService> logger) : IStripeWebhookService
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly ICartService _cartService = cartService;
    private readonly IStockService _stockService = stockService;
    private readonly IProcessedStripeEventRepository _processedEventRepository = processedEventRepository;
    private readonly StripeOptions _options = options.Value;
    private readonly ILogger<StripeWebhookService> _logger = logger;

    /// <inheritdoc />
    public async Task<WebhookHandlingResult> HandleWebhookEventAsync(string jsonPayload, string? stripeSignatureHeader)
    {
        Event stripeEvent;

        // 1. Signature Verification
        try
        {
            var isSecretConfigured = !string.IsNullOrWhiteSpace(_options.WebhookSecret) &&
                                     !_options.WebhookSecret.Contains("YOUR_", StringComparison.OrdinalIgnoreCase);

            if (isSecretConfigured)
            {
                if (string.IsNullOrWhiteSpace(stripeSignatureHeader))
                {
                    _logger.LogWarning("Missing Stripe-Signature header on webhook request.");
                    return new WebhookHandlingResult(WebhookResultStatus.InvalidSignature, "Missing Stripe-Signature header.");
                }

                stripeEvent = EventUtility.ConstructEvent(jsonPayload, stripeSignatureHeader, _options.WebhookSecret);
            }
            else
            {
                // Unconfigured / sandbox simulation: parse payload directly
                stripeEvent = EventUtility.ParseEvent(jsonPayload, throwOnApiVersionMismatch: false);
            }
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Stripe webhook signature verification failed.");
            return new WebhookHandlingResult(WebhookResultStatus.InvalidSignature, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to parse Stripe webhook event payload.");
            return new WebhookHandlingResult(WebhookResultStatus.Error, ex.Message);
        }

        // 2. Idempotency Check
        if (await _processedEventRepository.ExistsAsync(stripeEvent.Id))
        {
            _logger.LogInformation("Stripe event {EventId} ({Type}) already processed. Skipping.", stripeEvent.Id, stripeEvent.Type);
            return new WebhookHandlingResult(
                WebhookResultStatus.AlreadyProcessed,
                "Event already processed.",
                stripeEvent.Id,
                stripeEvent.Type);
        }

        // 3. Event Handling
        switch (stripeEvent.Type)
        {
            case "checkout.session.completed":
                return await HandleCheckoutSessionCompletedAsync(stripeEvent);

            case "checkout.session.expired":
                _logger.LogInformation("Checkout session expired: {EventId}. Order remains Unpaid.", stripeEvent.Id);
                await RecordProcessedEventAsync(stripeEvent.Id, stripeEvent.Type);
                return new WebhookHandlingResult(WebhookResultStatus.Success, "Checkout session expired.", stripeEvent.Id, stripeEvent.Type);

            case "payment_intent.payment_failed":
                _logger.LogInformation("PaymentIntent payment failed: {EventId}. Order remains Unpaid.", stripeEvent.Id);
                await RecordProcessedEventAsync(stripeEvent.Id, stripeEvent.Type);
                return new WebhookHandlingResult(WebhookResultStatus.Success, "Payment failed recorded.", stripeEvent.Id, stripeEvent.Type);

            default:
                _logger.LogDebug("Unhandled Stripe event type {Type} ({EventId}).", stripeEvent.Type, stripeEvent.Id);
                await RecordProcessedEventAsync(stripeEvent.Id, stripeEvent.Type);
                return new WebhookHandlingResult(WebhookResultStatus.IgnoredEvent, $"Event {stripeEvent.Type} ignored.", stripeEvent.Id, stripeEvent.Type);
        }
    }

    private async Task<WebhookHandlingResult> HandleCheckoutSessionCompletedAsync(Event stripeEvent)
    {
        if (stripeEvent.Data.Object is not Session session)
        {
            _logger.LogError("Stripe event {EventId} data object was not a Session.", stripeEvent.Id);
            return new WebhookHandlingResult(WebhookResultStatus.Error, "Invalid session data.", stripeEvent.Id, stripeEvent.Type);
        }

        // An order becomes Paid ONLY upon payment confirmation from the processor
        if (!string.Equals(session.PaymentStatus, "paid", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning(
                "Checkout session {SessionId} payment status is '{Status}' (not 'paid'). Order remains Unpaid.",
                session.Id, session.PaymentStatus);

            await RecordProcessedEventAsync(stripeEvent.Id, stripeEvent.Type);
            return new WebhookHandlingResult(
                WebhookResultStatus.Success,
                $"Session payment status '{session.PaymentStatus}' is not paid; left Unpaid.",
                stripeEvent.Id,
                stripeEvent.Type);
        }

        // Resolve order ID
        Order? order = null;

        if (session.Metadata != null && session.Metadata.TryGetValue("orderId", out var orderIdStr) && ulong.TryParse(orderIdStr, out var orderId))
        {
            order = await _orderRepository.GetWithItemsAsync(orderId);
        }

        if (order == null && ulong.TryParse(session.ClientReferenceId, out var clientRefId))
        {
            order = await _orderRepository.GetWithItemsAsync(clientRefId);
        }

        if (order == null)
        {
            order = await _orderRepository.GetByStripeSessionIdAsync(session.Id);
        }

        if (order == null)
        {
            _logger.LogError("Could not find order corresponding to Stripe Checkout Session {SessionId}.", session.Id);
            await RecordProcessedEventAsync(stripeEvent.Id, stripeEvent.Type);
            return new WebhookHandlingResult(
                WebhookResultStatus.OrderNotFound,
                $"Order not found for session {session.Id}",
                stripeEvent.Id,
                stripeEvent.Type);
        }

        // Idempotency: if already paid, do not re-process
        if (order.Status == OrderStatusType.Paid)
        {
            _logger.LogInformation("Order {OrderNumber} is already marked as Paid.", order.OrderNumber);
            await RecordProcessedEventAsync(stripeEvent.Id, stripeEvent.Type);
            return new WebhookHandlingResult(
                WebhookResultStatus.Success,
                "Order already marked as Paid.",
                stripeEvent.Id,
                stripeEvent.Type,
                order.Id);
        }

        // 1. Atomically decrease stock for confirmed order
        await _stockService.DecreaseStockForOrderAsync(order.Id);

        // 2. Transition order to Paid status
        order.Status = OrderStatusType.Paid;
        order.PaidAt = DateTime.UtcNow;
        order.StripePaymentIntentId = session.PaymentIntentId;
        order.UpdatedAt = DateTime.UtcNow;
        await _orderRepository.SaveAsync();

        // 3. Clear user's shopping cart
        try
        {
            await _cartService.ClearCartAsync(order.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to clear cart for user {UserId} after order {OrderNumber} was paid.", order.UserId, order.OrderNumber);
        }

        // 4. Record event to guarantee idempotency
        await RecordProcessedEventAsync(stripeEvent.Id, stripeEvent.Type);

        _logger.LogInformation(
            "Payment confirmed for order {OrderNumber} (ID: {OrderId}). Status updated to Paid, stock decreased, cart cleared.",
            order.OrderNumber, order.Id);

        return new WebhookHandlingResult(
            WebhookResultStatus.Success,
            $"Order {order.OrderNumber} marked as Paid.",
            stripeEvent.Id,
            stripeEvent.Type,
            order.Id);
    }

    private async Task RecordProcessedEventAsync(string eventId, string eventType)
    {
        try
        {
            var processed = new ProcessedStripeEvent
            {
                EventId = eventId,
                Type = eventType,
                ProcessedAt = DateTime.UtcNow
            };
            await _processedEventRepository.AddAsync(processed);
            await _processedEventRepository.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to record processed Stripe event {EventId} in database.", eventId);
        }
    }
}
