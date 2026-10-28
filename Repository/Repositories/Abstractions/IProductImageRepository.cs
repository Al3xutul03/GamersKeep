using Repository.Entity;
using Repository.Repositories.Generic;

namespace Repository.Repositories.Abstractions;

/// <summary>
/// ProductImage repository interface for product image information from the database.
/// </summary>
public interface IProductImageRepository : IBaseRepository<string, ProductImage>
{
}
