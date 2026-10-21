namespace Repository.Entity;

/// <summary>
/// URL for the images of a product page
/// </summary>
public class ProductImage
{
    /// <summary>
    /// ProductImage key. The URL of the wanted image.
    /// </summary>
    public string ImageUrl { get; set; } = null!;

    /// <summary>
    /// Id of the product.
    /// </summary>
    public ulong ProductId { get; set; }
    public Product? Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
