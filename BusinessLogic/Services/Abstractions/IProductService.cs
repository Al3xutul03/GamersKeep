using BusinessLogic.Models;
using BusinessLogic.Models.Product;
using BusinessLogic.Services.Generic;
using Repository.Entity;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// Product service implementation for product information from the database
/// </summary>
public interface IProductService
    : IBaseService<ulong, Product, ProductReadModel, ProductCreateModel, ProductUpdateModel>
{
    /// <summary>
    /// Retrieves the total number of pages available based on the provided page size and search filter.
    /// </summary>
    /// <param name="page_size">The number of items per page.</param>
    /// <param name="filter">The search filter to apply.</param>
    /// <returns>A task representing the asynchronous operation, with the result being the total number of pages.</returns>
    public Task<ulong> GetNoPages(ulong page_size, SearchFilterModel filter);

    /// <summary>
    /// Retrieves a paginated list of product listings based on the provided page number, page size, and search filter.
    /// </summary>
    /// <param name="page_no">The page number to retrieve.</param>
    /// <param name="page_size">The number of items per page.</param>
    /// <param name="filter">The search filter to apply.</param>
    /// <returns>A task representing the asynchronous operation, with the result being a collection of product listings.</returns>
    public Task<ICollection<ProductListingModel>> GetProductListingsAsync(ulong page_no, ulong page_size, SearchFilterModel filter);
}
