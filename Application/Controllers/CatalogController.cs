using Application.Models;
using BusinessLogic.Models;
using BusinessLogic.Models.Product;
using BusinessLogic.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class CatalogController(IProductService productService) : Controller
{
    private const string VariantSeparator = " – ";

    private static readonly Dictionary<string, string> VariantColors = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Violet"] = "#6b3fa0",
        ["Negru"] = "#1b1b1f",
        ["Albastru"] = "#2f6fb3",
        ["Roșu"] = "#b3362f",
        ["Verde"] = "#2f8f5b",
        ["Alb"] = "#f3f3f7"
    };

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] SearchFilterModel? filter, [FromQuery] ulong page = 1, [FromQuery] ulong pageSize = 12)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize < 1 ? 12 : pageSize;

        filter ??= new SearchFilterModel();

        ulong totalPages = 1;
        ICollection<ProductListingModel> products = new List<ProductListingModel>();

        try
        {
            totalPages = await productService.GetNoPages(pageSize, filter);
            products = await productService.GetProductListingsAsync(page, pageSize, filter) ?? new List<ProductListingModel>();
        }
        catch
        {
            totalPages = 1;
            products = new List<ProductListingModel>();
        }

        var viewModel = new CatalogViewModel
        {
            Products = products,
            Filter = filter,
            CurrentPage = page,
            TotalPages = totalPages == 0 ? 1 : totalPages,
            PageSize = pageSize
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(ulong id)
    {
        var product = await productService.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        var model = new ProductDetailsViewModel { Product = product };

        var separatorIndex = product.Name.IndexOf(VariantSeparator, StringComparison.Ordinal);
        if (separatorIndex > 0)
        {
            var baseName = product.Name[..separatorIndex];
            var prefix = baseName + VariantSeparator;

            var siblings = await productService.GetProductListingsAsync(
                1, 50, new SearchFilterModel(SearchQuery: baseName, IncludeOutOfStock: true));

            var variants = siblings
                .Where(p => p.Name.StartsWith(prefix, StringComparison.Ordinal))
                .OrderBy(p => p.Id)
                .Select(p =>
                {
                    var label = p.Name[prefix.Length..];
                    VariantColors.TryGetValue(label, out var color);

                    return new ProductVariantOption
                    {
                        Id = p.Id,
                        Label = label,
                        Color = color,
                        ImageUrl = p.FirstImageUrl,
                        IsCurrent = p.Id == product.Id,
                        InStock = p.Stock > 0
                    };
                })
                .ToList();

            if (variants.Count > 1)
            {
                model.Variants = variants;
            }
        }

        return View(model);
    }
}