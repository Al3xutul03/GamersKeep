using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

/// <summary>
/// ProductImage repository implementation for all product image information.
/// </summary>
/// <param name="context">The database context to use.</param>
public class ProductImageRepository(DbContext context) : BaseRepository<string, ProductImage>(context, "ImageUrl"), IProductImageRepository
{
}
