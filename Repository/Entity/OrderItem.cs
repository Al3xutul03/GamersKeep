namespace Repository.Entity;

/// <summary>
/// Database model for a single product line within an <see cref="Order"/>.
/// </summary>
public class OrderItem
{
    public ulong OrderId { get; set; }
    public Order? Order { get; set; }

    public ulong ProductId { get; set; }
    public Product? Product { get; set; }

    /// <summary>
    /// Snapshot of the product name at the time of ordering.
    /// </summary>
    public string ProductName { get; set; } = null!;

    /// <summary>
    /// Snapshot of the product unit price at the time of ordering. (in lei)
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Number of ordered units.
    /// </summary>
    public uint Quantity { get; set; }
}
