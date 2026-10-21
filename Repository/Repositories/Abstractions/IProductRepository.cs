using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories.Generic;

namespace Repository.Repositories.Abstractions;

/// <summary>
/// Product repository interface for product information from the database.
/// </summary>
public interface IProductRepository : IBaseRepository<ulong, Product>
{
    /// <summary>
    /// Get the total number of products in the database with optional includes.
    /// </summary>
    /// <param name="includes">A function to specify additional filters.</param>
    /// <returns>A task representing the asynchronous operation, with the result being the total number of products.</returns>
    public Task<ulong> GetNoProductsAsync(Func<IQueryable<Product>, IQueryable<Product>>? includes = null);

    /// <summary>
    /// Get the total number of pages of products in the database based on the specified page size, with optional includes.
    /// </summary>
    /// <param name="page_size">The number of products per page.</param>
    /// <param name="includes">A function to specify additional filters.</param>
    /// <returns>A task representing the asynchronous operation, with the result being the total number of pages.</returns>
    public Task<ulong> GetNoPagesAsync(ulong page_size, Func<IQueryable<Product>, IQueryable<Product>>? includes = null);

    /// <summary>
    /// Get a page of products from the database with optional includes.
    /// </summary>
    /// <param name="page_no">The page number to retrieve.</param>
    /// <param name="page_size">The number of products per page.</param>
    /// <param name="behavior">The include behavior for related entities.</param>
    /// <param name="includes">A function to specify additional includes and/or filters.</param>
    /// <returns>A task representing the asynchronous operation, with a collection of products.</returns>
    public Task<IEnumerable<Product>> GetProductPageAsync(ulong page_no, ulong page_size,
        IncludeBehaviour behavior, Func<IQueryable<Product>, IQueryable<Product>>? includes = null);
}
