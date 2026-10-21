using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Enums.Types;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

/// <summary>
/// Category repository implementation for all category information.
/// </summary>
/// <param name="context">The database context to use.</param>
public class CategoryRepository(DbContext context) : BaseRepository<CategoryType, Category>(context, "Type"), ICategoryRepository
{
}
