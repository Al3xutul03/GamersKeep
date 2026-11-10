using BusinessLogic.Models.Product;

namespace Application.Models;

public class ProductVariantOption
{
    public ulong Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public string? Color { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsCurrent { get; set; }
    public bool InStock { get; set; }
}

public class ProductDetailsViewModel
{
    public ProductReadModel Product { get; set; } = null!;
    public List<ProductVariantOption> Variants { get; set; } = new();
}