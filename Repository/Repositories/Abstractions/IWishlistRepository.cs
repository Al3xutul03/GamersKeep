using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories.Generic;

namespace Repository.Repositories.Abstractions;

public interface IWishlistRepository : IBaseRepository<(ulong UserId, ulong ProductId), WishlistEntry>
{
    public Task<IEnumerable<WishlistEntry>> GetByUserIdAsync(
        ulong userId,
        IncludeBehaviour behavior = IncludeBehaviour.NoInclude,
        Func<IQueryable<WishlistEntry>, IQueryable<WishlistEntry>>? includes = null);
}