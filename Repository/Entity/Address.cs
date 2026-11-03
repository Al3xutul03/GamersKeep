namespace Repository.Entity;

/// <summary>
/// Database model for a delivery address saved by a user.
/// </summary>
public class Address
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public ulong Id { get; set; }

    /// <summary>
    /// The owner of the address.
    /// </summary>
    public ulong UserId { get; set; }
    public User? User { get; set; }

    /// <summary>
    /// Optional friendly label (e.g. "Acasă", "Birou").
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Full name of the recipient.
    /// </summary>
    public string FullName { get; set; } = null!;

    /// <summary>
    /// Contact phone number of the recipient.
    /// </summary>
    public string Phone { get; set; } = null!;

    /// <summary>
    /// Street, number, building, apartment etc.
    /// </summary>
    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    /// <summary>
    /// County (județ).
    /// </summary>
    public string County { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    /// <summary>
    /// Whether this is the user's default delivery address.
    /// </summary>
    public bool IsDefault { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
