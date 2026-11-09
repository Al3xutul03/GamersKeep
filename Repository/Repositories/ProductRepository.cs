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
    private static readonly object _inMemoryLock = new();

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

    /// <inheritdoc />
    public async Task<(bool Success, ulong? FailedProductId)> TryAtomicDecrementStockBatchAsync(IReadOnlyList<(ulong ProductId, uint Quantity)> items)
    {
        if (items.Count == 0)
        {
            return (true, null);
        }

        // Lock in consistent order to prevent deadlocks between concurrent checkouts
        var ordered = items.OrderBy(i => i.ProductId).ToList();

        if (_context.Database.IsRelational())
        {
            var executionStrategy = _context.Database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                var now = DateTime.UtcNow;

                foreach (var (productId, quantity) in ordered)
                {
                    // Conditional update: only succeeds if stock is still >= quantity
                    var rows = await _context.Database.ExecuteSqlInterpolatedAsync(
                        $"UPDATE `products` SET `Stock` = `Stock` - {quantity}, `UpdatedAt` = {now} WHERE `Id` = {productId} AND `Stock` >= {quantity} AND `DeletedAt` IS NULL");

                    if (rows == 0)
                    {
                        await transaction.RollbackAsync();
                        return (false, (ulong?)productId);
                    }
                }

                await transaction.CommitAsync();
                return (true, (ulong?)null);
            });
        }

        // In-memory fallback (used for fast unit tests without MySQL)
        lock (_inMemoryLock)
        {
            var now = DateTime.UtcNow;
            var productEntities = new List<(Product Product, uint Quantity)>();

            foreach (var (productId, quantity) in ordered)
            {
                var product = _dbSet.FirstOrDefault(p => p.Id == productId && p.DeletedAt == null);
                if (product == null || product.Stock < quantity)
                {
                    return (false, productId);
                }
                productEntities.Add((product, quantity));
            }

            foreach (var (product, quantity) in productEntities)
            {
                product.Stock -= quantity;
                product.UpdatedAt = now;
            }

            _context.SaveChanges();
            return (true, null);
        }
    }

    /// <inheritdoc />
    public async Task AtomicIncrementStockBatchAsync(IReadOnlyList<(ulong ProductId, uint Quantity)> items)
    {
        if (items.Count == 0)
        {
            return;
        }

        var ordered = items.OrderBy(i => i.ProductId).ToList();

        if (_context.Database.IsRelational())
        {
            var executionStrategy = _context.Database.CreateExecutionStrategy();
            await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                var now = DateTime.UtcNow;

                foreach (var (productId, quantity) in ordered)
                {
                    await _context.Database.ExecuteSqlInterpolatedAsync(
                        $"UPDATE `products` SET `Stock` = `Stock` + {quantity}, `UpdatedAt` = {now} WHERE `Id` = {productId} AND `DeletedAt` IS NULL");
                }

                await transaction.CommitAsync();
            });
            return;
        }

        // In-memory fallback
        lock (_inMemoryLock)
        {
            var now = DateTime.UtcNow;
            foreach (var (productId, quantity) in ordered)
            {
                var product = _dbSet.FirstOrDefault(p => p.Id == productId && p.DeletedAt == null);
                if (product != null)
                {
                    product.Stock += quantity;
                    product.UpdatedAt = now;
                }
            }
            _context.SaveChanges();
        }
    }

    /// <inheritdoc />
    public async Task<IReadOnlyDictionary<ulong, uint>> GetStockForProductsAsync(IEnumerable<ulong> productIds)
    {
        var idList = productIds.Distinct().ToList();
        var list = await _dbSet
            .Where(p => idList.Contains(p.Id) && p.DeletedAt == null)
            .Select(p => new { p.Id, p.Stock })
            .ToListAsync();

        return list.ToDictionary(p => p.Id, p => p.Stock);
    }
}
