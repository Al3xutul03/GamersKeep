using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

/// <summary>
/// Order repository implementation for customer orders.
/// </summary>
/// <param name="context">The database context to use.</param>
public class OrderRepository(DbContext context) : BaseRepository<ulong, Order>(context, "Id"), IOrderRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Order>> GetByUserIdAsync(ulong userId)
    {
        return await _dbSet
            .Include(o => o.Items)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Order?> GetUserOrderByNumberAsync(ulong userId, string orderNumber)
    {
        return await _dbSet
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderNumber == orderNumber);
    }

    /// <inheritdoc />
    public async Task<Order?> GetByStripeSessionIdAsync(string sessionId)
    {
        return await _dbSet
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.StripeCheckoutSessionId == sessionId);
    }

    /// <inheritdoc />
    public async Task<Order?> GetWithDetailsAsync(ulong orderId)
    {
        return await _dbSet
            .Include(o => o.Items)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    /// <inheritdoc />
    public async Task<bool> OrderNumberExistsAsync(string orderNumber)
    {
        return await _dbSet.AnyAsync(o => o.OrderNumber == orderNumber);
    }
}
