using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Enums.Types;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

/// <summary>
/// ProductCategory repository implementation for all product-category relationship information.
/// </summary>
/// <param name="context">The database context to use.</param>
public class ProductCategoryRepository(DbContext context)
    : BaseRepository<(ulong ProductId, CategoryType CategoryId), ProductCategory>(context, "ProductId", "CategoryId"), IProductCategoryRepository
{
}
