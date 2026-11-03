using Repository.Entity;
using Repository.Enums.Types;
using Repository.Repositories.Generic;

namespace Repository.Repositories.Abstractions;

/// <summary>
/// ProductCategory repository interface for product-category relationship information from the database.
/// </summary>
public interface IProductCategoryRepository : IBaseRepository<(ulong ProductId, CategoryType CategoryId), ProductCategory>
{
}
