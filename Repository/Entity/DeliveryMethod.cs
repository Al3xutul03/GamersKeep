namespace Repository.Entity;

/// <summary>
/// Database model for a delivery method that can be chosen at checkout.
/// </summary>
public class DeliveryMethod
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public ulong Id { get; set; }

    /// <summary>
    /// Display name of the delivery method.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Short description shown at checkout.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Human readable delivery estimate (e.g. "2-4 zile lucrătoare").
    /// </summary>
    public string EstimatedDelivery { get; set; } = null!;

    /// <summary>
    /// Cost of the delivery method. (in lei)
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Whether the method can currently be selected at checkout.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Display order at checkout (ascending).
    /// </summary>
    public int SortOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Orders placed with this delivery method.
    /// </summary>
    public ICollection<Order> Orders { get; set; } = [];
}
