using Microsoft.EntityFrameworkCore;
using Repository.Builders;
using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

/// <summary>
/// Product repository implementation for all product information.
/// </summary>
/// <param name="context">The database context to use.</param>
public class ProductRepository(DbContext context) : BaseRepository<ulong, Product>(context, "Id"), IProductRepository
{
    /// <inheritdoc />
    public async Task<ulong> GetNoProductsAsync(Func<IQueryable<Product>, IQueryable<Product>>? includes = null)
    {
        IQueryable<Product> query = new QueryBuilder<Product>(_dbSet)
            .AddIncludes(includes)
            .AddBehavior(IncludeBehaviour.GivenIncludes)
            .Build();

        return (ulong)await query.CountAsync();
    }

    /// <inheritdoc />
    public async Task<ulong> GetNoPagesAsync(ulong page_size, Func<IQueryable<Product>, IQueryable<Product>>? includes = null)
    {
        var noProducts = await GetNoProductsAsync(includes);
        return (ulong)Math.Ceiling((double)noProducts / page_size);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> GetProductPageAsync(ulong page_no, ulong page_size,
        IncludeBehaviour behavior, Func<IQueryable<Product>, IQueryable<Product>>? includes = null)
    {
        IQueryable<Product> query = new QueryBuilder<Product>(_dbSet)
            .AddIncludes(includes)
            .AddBehavior(behavior)
            .Build();

        return await query.Skip((int)((page_no - 1) * page_size)).Take((int)page_size).ToListAsync();
    }
}
