using Repository.Entity;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// Result containing Stripe Checkout Session information.
/// </summary>
public record PaymentSessionResult(
    string SessionId,
    string CheckoutUrl);

/// <summary>
/// Gateway interface abstracting payment processor checkout session creation.
/// </summary>
public interface IPaymentGateway
{
    /// <summary>
    /// Creates a Stripe Checkout Session for the specified order and sets the session ID on the order.
    /// </summary>
    /// <param name="order">The order to be paid.</param>
    /// <param name="customerEmail">The email address of the customer.</param>
    /// <returns>The created session details including the redirect URL.</returns>
    public Task<PaymentSessionResult> CreateCheckoutSessionAsync(Order order, string customerEmail);
}
