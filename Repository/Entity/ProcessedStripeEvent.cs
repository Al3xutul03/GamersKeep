namespace Repository.Entity;

/// <summary>
/// Record of a Stripe webhook event that has already been handled.
/// Stripe may deliver the same event more than once, so this table makes
/// webhook processing idempotent.
/// </summary>
public class ProcessedStripeEvent
{
    /// <summary>
    /// Stripe event id (e.g. "evt_..."). Primary key.
    /// </summary>
    public string EventId { get; set; } = null!;

    /// <summary>
    /// Stripe event type (e.g. "checkout.session.completed").
    /// </summary>
    public string Type { get; set; } = null!;

    public DateTime ProcessedAt { get; set; }
}
