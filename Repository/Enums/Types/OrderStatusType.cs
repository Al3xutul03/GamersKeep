namespace Repository.Enums.Types;

/// <summary>
/// Lifecycle status of an <see cref="Entity.Order"/>.
/// </summary>
public enum OrderStatusType
{
    /// <summary>
    /// The order was placed but no payment confirmation has been received
    /// from the payment processor yet. A failed payment leaves the order in this state.
    /// </summary>
    Unpaid,

    /// <summary>
    /// The payment processor confirmed the payment (via webhook).
    /// Stock has been deducted for the order items.
    /// </summary>
    Paid,

    /// <summary>
    /// The order was cancelled. If it had been paid, the stock is restored
    /// and the payment is refunded.
    /// </summary>
    Cancelled,
}
