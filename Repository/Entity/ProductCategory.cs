using Repository.Enums.Types;

namespace Repository.Entity;

/// <summary>
/// Intermediary table for products and their categories
/// </summary>
public class ProductCategory
{
    public ulong ProductId { get; set; }
    public Product? Product { get; set; }
    public CategoryType CategoryId { get; set; }
    public Category? Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
