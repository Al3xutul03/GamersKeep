namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// Status of webhook event processing.
/// </summary>
public enum WebhookResultStatus
{
    Success,
    AlreadyProcessed,
    InvalidSignature,
    IgnoredEvent,
    OrderNotFound,
    Error
}

/// <summary>
/// Result of a Stripe webhook event processing attempt.
/// </summary>
public record WebhookHandlingResult(
    WebhookResultStatus Status,
    string? Message = null,
    string? EventId = null,
    string? EventType = null,
    ulong? OrderId = null);

/// <summary>
/// Service managing the processing, signature validation, and idempotency of incoming Stripe webhooks.
/// </summary>
public interface IStripeWebhookService
{
    /// <summary>
    /// Processes a raw Stripe webhook request payload.
    /// Validates HMAC signature, checks idempotency, decrements stock atomically on payment confirmation,
    /// marks order as Paid, and clears the user's cart.
    /// </summary>
    /// <param name="jsonPayload">Raw HTTP request body.</param>
    /// <param name="stripeSignatureHeader">The Stripe-Signature header value.</param>
    /// <returns>A result describing the processing status.</returns>
    public Task<WebhookHandlingResult> HandleWebhookEventAsync(string jsonPayload, string? stripeSignatureHeader);
}
