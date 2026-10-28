using Microsoft.EntityFrameworkCore;
using Repository.Builders;
using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

/// <summary>
/// CartEntry repository implementation for all user cart entry information.
/// </summary>
/// <param name="context">The database context to use.</param>
public class CartEntryRepository(DbContext context)
    : BaseRepository<(ulong UserId, ulong ProductId), CartEntry>(context, "UserId", "ProductId"), ICartEntryRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<CartEntry>> GetByUserIdAsync(
        ulong userId,
        IncludeBehaviour behavior = IncludeBehaviour.NoInclude,
        Func<IQueryable<CartEntry>, IQueryable<CartEntry>>? includes = null)
    {
        IQueryable<CartEntry> query = new QueryBuilder<CartEntry>(_dbSet)
            .AddIncludes(includes)
            .AddBehavior(behavior)
            .Build();

        return await query.Where(ce => ce.UserId == userId).ToListAsync();
    }
}
