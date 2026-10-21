using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

/// <summary>
/// ProductCategory repository implementation for all product-category relationship information.
/// </summary>
/// <param name="context">The database context to use.</param>
public class ProductCategoryRepository(DbContext context) : BaseRepository<ulong, ProductCategory>(context, "ProductId"), IProductCategoryRepository
{
}
