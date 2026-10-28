using BusinessLogic.Models;
using BusinessLogic.Models.Product;

namespace Application.Models;

public class CatalogViewModel
{
    public ICollection<ProductListingModel> Products { get; set; } = new List<ProductListingModel>();
    public SearchFilterModel Filter { get; set; } = new SearchFilterModel();
    public ulong CurrentPage { get; set; } = 1;
    public ulong TotalPages { get; set; } = 1;
    public ulong PageSize { get; set; } = 12;
}