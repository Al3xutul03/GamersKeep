using BusinessLogic.Options;
using BusinessLogic.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repository.Entity;
using Repository.Repositories.Abstractions;
using Stripe;
using Stripe.Checkout;

namespace BusinessLogic.Services;

/// <summary>
/// Implementation of <see cref="IPaymentGateway"/> using the Stripe Checkout hosted redirect flow.
/// Automatically falls back to a sandbox simulation if real Stripe API keys have not been configured yet.
/// </summary>
public class StripePaymentGateway(
    IOptions<StripeOptions> options,
    IOrderRepository orderRepository,
    ILogger<StripePaymentGateway> logger) : IPaymentGateway
{
    private readonly StripeOptions _options = options.Value;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly ILogger<StripePaymentGateway> _logger = logger;

    /// <inheritdoc />
    public async Task<PaymentSessionResult> CreateCheckoutSessionAsync(Order order, string customerEmail)
    {
        // 1. Simulation fallback for local development when credentials are not configured
        if (!_options.IsConfigured)
        {
            var mockSessionId = $"cs_sim_{order.OrderNumber}_{Guid.NewGuid().ToString("N")[..8]}";
            _logger.LogWarning(
                "Stripe API keys are not configured. Running in sandbox simulation mode. Session: {SessionId}",
                mockSessionId);

            order.StripeCheckoutSessionId = mockSessionId;
            order.UpdatedAt = DateTime.UtcNow;
            await _orderRepository.SaveAsync();

            var simulatedRedirectUrl = _options.SuccessUrl.Replace("{CHECKOUT_SESSION_ID}", mockSessionId);
            return new PaymentSessionResult(mockSessionId, simulatedRedirectUrl);
        }

        // 2. Real Stripe Checkout session creation
        StripeConfiguration.ApiKey = _options.SecretKey;

        var lineItems = new List<SessionLineItemOptions>();

        foreach (var item in order.Items)
        {
            lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = (order.Currency ?? "ron").ToLowerInvariant(),
                    UnitAmount = (long)Math.Round(item.UnitPrice * 100m, MidpointRounding.AwayFromZero),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.ProductName
                    }
                },
                Quantity = item.Quantity
            });
        }

        if (order.DeliveryCost > 0)
        {
            lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = (order.Currency ?? "ron").ToLowerInvariant(),
                    UnitAmount = (long)Math.Round(order.DeliveryCost * 100m, MidpointRounding.AwayFromZero),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = $"Livrare: {order.DeliveryMethodName}"
                    }
                },
                Quantity = 1
            });
        }

        var sessionOptions = new SessionCreateOptions
        {
            Mode = "payment",
            CustomerEmail = customerEmail,
            ClientReferenceId = order.Id.ToString(),
            Metadata = new Dictionary<string, string>
            {
                { "orderId", order.Id.ToString() },
                { "orderNumber", order.OrderNumber },
                { "userId", order.UserId.ToString() }
            },
            LineItems = lineItems,
            SuccessUrl = _options.SuccessUrl,
            CancelUrl = _options.CancelUrl.Replace("{ORDER_ID}", order.Id.ToString())
        };

        var service = new SessionService();
        var session = await service.CreateAsync(sessionOptions);

        order.StripeCheckoutSessionId = session.Id;
        order.UpdatedAt = DateTime.UtcNow;
        await _orderRepository.SaveAsync();

        _logger.LogInformation(
            "Created Stripe Checkout Session {SessionId} for order {OrderNumber}. Redirect: {Url}",
            session.Id, order.OrderNumber, session.Url);

        return new PaymentSessionResult(session.Id, session.Url);
    }
}
