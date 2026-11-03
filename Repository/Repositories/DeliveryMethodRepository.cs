using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

/// <summary>
/// DeliveryMethod repository implementation for the delivery options available at checkout.
/// </summary>
/// <param name="context">The database context to use.</param>
public class DeliveryMethodRepository(DbContext context)
    : BaseRepository<ulong, DeliveryMethod>(context, "Id"), IDeliveryMethodRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<DeliveryMethod>> GetActiveAsync()
    {
        return await _dbSet
            .Where(dm => dm.IsActive && dm.DeletedAt == null)
            .OrderBy(dm => dm.SortOrder)
            .ToListAsync();
    }
}
