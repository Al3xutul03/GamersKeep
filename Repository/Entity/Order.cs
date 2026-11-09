using Repository.Enums.Types;

namespace Repository.Entity;

/// <summary>
/// Database model for a customer order.
/// Prices, delivery method and shipping address are stored as snapshots so the
/// order history stays accurate even if products, delivery methods or saved
/// addresses change later.
/// </summary>
public class Order
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public ulong Id { get; set; }

    /// <summary>
    /// Public, human readable order number shown to the customer (e.g. "GK-20261103-7KQ2XM").
    /// </summary>
    public string OrderNumber { get; set; } = null!;

    /// <summary>
    /// The customer who placed the order.
    /// </summary>
    public ulong UserId { get; set; }
    public User? User { get; set; }

    /// <summary>
    /// Current lifecycle status of the order.
    /// </summary>
    public OrderStatusType Status { get; set; } = OrderStatusType.Unpaid;

    /// <summary>
    /// The chosen delivery method.
    /// </summary>
    public ulong DeliveryMethodId { get; set; }
    public DeliveryMethod? DeliveryMethod { get; set; }

    /// <summary>
    /// Snapshot of the delivery method name at the time of ordering.
    /// </summary>
    public string DeliveryMethodName { get; set; } = null!;

    /// <summary>
    /// Snapshot of the delivery cost at the time of ordering. (in lei)
    /// </summary>
    public decimal DeliveryCost { get; set; }

    /// <summary>
    /// Sum of all order item line totals, without delivery. (in lei)
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    /// Amount charged to the customer: <see cref="Subtotal"/> + <see cref="DeliveryCost"/>. (in lei)
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// ISO 4217 currency code.
    /// </summary>
    public string Currency { get; set; } = "RON";

    // ---- Shipping address snapshot ----

    public string ShippingFullName { get; set; } = null!;

    public string ShippingPhone { get; set; } = null!;

    public string ShippingStreet { get; set; } = null!;

    public string ShippingCity { get; set; } = null!;

    public string ShippingCounty { get; set; } = null!;

    public string ShippingPostalCode { get; set; } = null!;

    // ---- Payment ----

    /// <summary>
    /// Id of the Stripe Checkout Session created for this order.
    /// </summary>
    public string? StripeCheckoutSessionId { get; set; }

    /// <summary>
    /// Id of the Stripe PaymentIntent, set once the payment is confirmed.
    /// </summary>
    public string? StripePaymentIntentId { get; set; }

    /// <summary>
    /// Whether the stock for this order's items has been deducted.
    /// Guards stock restoration so it happens at most once.
    /// </summary>
    public bool StockDeducted { get; set; }

    /// <summary>
    /// When the payment processor confirmed the payment.
    /// </summary>
    public DateTime? PaidAt { get; set; }

    /// <summary>
    /// When the order was cancelled.
    /// </summary>
    public DateTime? CancelledAt { get; set; }

    /// <summary>
    /// When the payment was refunded (set for cancelled orders that had been paid).
    /// </summary>
    public DateTime? RefundedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// The products contained in the order.
    /// </summary>
    public ICollection<OrderItem> Items { get; set; } = [];
}
