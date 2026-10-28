using Repository.Entity;
using Repository.Repositories.Generic;

namespace Repository.Repositories.Abstractions;

/// <summary>
/// CartEntry repository interface for user cart entry information from the database.
/// </summary>
public interface ICartEntryRepository : IBaseRepository<(ulong UserId, ulong ProductId), CartEntry>
{
}
