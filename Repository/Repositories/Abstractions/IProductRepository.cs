using Repository.Entity;
using Repository.Repositories.Generic;

namespace Repository.Repositories.Abstractions;

/// <summary>
/// Product repository interface for product information from the database.
/// </summary>
public interface IProductRepository : IBaseRepository<ulong, Product>
{
}
