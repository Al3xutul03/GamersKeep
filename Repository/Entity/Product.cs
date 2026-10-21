using Repository.Enums.Types;

namespace Repository.Entity;

/// <summary>
/// Database model for product information
/// </summary>
public class Product
{
    /// <summary>
    /// Primary key
    /// </summary>
    public ulong Id { get; set; }

    /// <summary>
    /// Name of the product.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Age rating of the product.
    /// </summary>
    public AgeRatingType AgeRating { get; set; }

    /// <summary>
    /// Categories of a product.
    /// </summary>
    public ICollection<ProductCategory>? Categories { get; set; }

    /// <summary>
    /// Description about the product.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Paths of the product images.
    /// </summary>
    public ICollection<ProductImage>? Images { get; set; }

    /// <summary>
    /// Price of the product. (in lei)
    /// </summary>
    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}