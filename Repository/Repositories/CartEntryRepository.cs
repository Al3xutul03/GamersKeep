using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

/// <summary>
/// CartEntry repository implementation for all user cart entry information.
/// </summary>
/// <param name="context">The database context to use.</param>
public class CartEntryRepository(DbContext context)
    : BaseRepository<(ulong UserId, ulong ProductId), CartEntry>(context, "UserId", "ProductId"), ICartEntryRepository
{
}
