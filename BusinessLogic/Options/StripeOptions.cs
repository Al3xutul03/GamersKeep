namespace BusinessLogic.Options;

/// <summary>
/// Configuration settings for the Stripe Payment processor and webhook integration.
/// </summary>
public class StripeOptions
{
    public const string SectionName = "Stripe";

    public string PublishableKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string WebhookSecret { get; set; } = string.Empty;

    public string SuccessUrl { get; set; } = "http://localhost:8080/Checkout/Confirmation?session_id={CHECKOUT_SESSION_ID}";
    public string CancelUrl { get; set; } = "http://localhost:8080/Checkout/Cancel?orderId={ORDER_ID}";

    /// <summary>
    /// Indicates whether real Stripe API keys have been provided.
    /// When false or set to placeholder values, the payment gateway runs in simulation mode.
    /// </summary>
    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(SecretKey) &&
        !SecretKey.Contains("YOUR_", StringComparison.OrdinalIgnoreCase) &&
        !SecretKey.Contains("placeholder", StringComparison.OrdinalIgnoreCase) &&
        !SecretKey.Contains("mock", StringComparison.OrdinalIgnoreCase) &&
        SecretKey.StartsWith("sk_", StringComparison.OrdinalIgnoreCase) &&
        SecretKey.Length > 20;
}
