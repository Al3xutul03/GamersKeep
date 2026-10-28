using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories.Generic;

namespace Repository.Repositories.Abstractions;

/// <summary>
/// CartEntry repository interface for user cart entry information from the database.
/// </summary>
public interface ICartEntryRepository : IBaseRepository<(ulong UserId, ulong ProductId), CartEntry>
{
    /// <summary>
    /// Read all cart entries belonging to a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="behavior">Behavior describing what joins the query should include.</param>
    /// <param name="includes">Specific includes to be used.</param>
    /// <returns>A collection of cart entries for the user.</returns>
    public Task<IEnumerable<CartEntry>> GetByUserIdAsync(
        ulong userId,
        IncludeBehaviour behavior = IncludeBehaviour.NoInclude,
        Func<IQueryable<CartEntry>, IQueryable<CartEntry>>? includes = null);
}
