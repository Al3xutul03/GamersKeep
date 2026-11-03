using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

/// <summary>
/// Address repository implementation for users' saved delivery addresses.
/// </summary>
/// <param name="context">The database context to use.</param>
public class AddressRepository(DbContext context) : BaseRepository<ulong, Address>(context, "Id"), IAddressRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Address>> GetByUserIdAsync(ulong userId)
    {
        return await _dbSet
            .Where(a => a.UserId == userId && a.DeletedAt == null)
            .OrderByDescending(a => a.IsDefault)
            .ThenByDescending(a => a.UpdatedAt)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Address?> GetUserAddressAsync(ulong userId, ulong addressId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(a => a.Id == addressId && a.UserId == userId && a.DeletedAt == null);
    }
}
