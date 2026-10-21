using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

/// <summary>
/// Product repository implementation for all product information.
/// </summary>
/// <param name="context">The database context to use.</param>
public class ProductRepository(DbContext context) : BaseRepository<ulong, Product>(context, "Id"), IProductRepository
{
}
