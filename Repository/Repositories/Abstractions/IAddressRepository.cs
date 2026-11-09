using Repository.Entity;
using Repository.Repositories.Generic;

namespace Repository.Repositories.Abstractions;

/// <summary>
/// Address repository interface for users' saved delivery addresses.
/// </summary>
public interface IAddressRepository : IBaseRepository<ulong, Address>
{
    /// <summary>
    /// Read all non-deleted addresses of a user, default address first.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A collection of the user's addresses.</returns>
    public Task<IEnumerable<Address>> GetByUserIdAsync(ulong userId);

    /// <summary>
    /// Read a non-deleted address only if it belongs to the given user.
    /// Used for ownership checks so users cannot access other users' addresses.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="addressId">The ID of the address.</param>
    /// <returns>The address, or <c>null</c> if not found or not owned by the user.</returns>
    public Task<Address?> GetUserAddressAsync(ulong userId, ulong addressId);
}
