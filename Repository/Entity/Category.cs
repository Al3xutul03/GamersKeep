using Repository.Enums.Types;

namespace Repository.Entity;

/// <summary>
/// Categories that can be assigned to multiple products
/// </summary>
public class Category
{
    public CategoryType Type { get; set; }
    public string Description { get; set; } = null!;
    public ICollection<ProductCategory>? Products { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
