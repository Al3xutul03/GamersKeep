using Repository.Entity;
using Repository.Enums.Types;
using Repository.Repositories.Generic;

namespace Repository.Repositories.Abstractions;

/// <summary>
/// Category repository interface for category information from the database.
/// </summary>
public interface ICategoryRepository : IBaseRepository<CategoryType, Category>
{
}
