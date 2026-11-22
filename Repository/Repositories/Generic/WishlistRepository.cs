using Microsoft.EntityFrameworkCore;
using Repository.Builders;
using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

public class WishlistRepository(DbContext context)
    : BaseRepository<(ulong UserId, ulong ProductId), WishlistEntry>(context, "UserId", "ProductId"), IWishlistRepository
{
    public async Task<IEnumerable<WishlistEntry>> GetByUserIdAsync(
        ulong userId,
        IncludeBehaviour behavior = IncludeBehaviour.NoInclude,
        Func<IQueryable<WishlistEntry>, IQueryable<WishlistEntry>>? includes = null)
    {
        IQueryable<WishlistEntry> query = new QueryBuilder<WishlistEntry>(_dbSet)
            .AddIncludes(includes)
            .AddBehavior(behavior)
            .Build();

        return await query.Where(w => w.UserId == userId).ToListAsync();
    }
}