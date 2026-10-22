using Application.Models;
using BusinessLogic.Models;
using BusinessLogic.Models.Product;
using BusinessLogic.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class CatalogController(IProductService productService) : Controller
{
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

        return View(product);
    }
}