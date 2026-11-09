using BusinessLogic.Models.Address;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// Service managing users' saved delivery addresses.
/// </summary>
public interface IAddressService
{
    /// <summary>
    /// Retrieves all saved delivery addresses for a user, default address first.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A list of the user's addresses.</returns>
    public Task<IReadOnlyList<AddressDto>> GetUserAddressesAsync(ulong userId);

    /// <summary>
    /// Retrieves a specific address only if it belongs to the specified user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="addressId">The ID of the address.</param>
    /// <returns>The address, or <c>null</c> if not found or not owned by user.</returns>
    public Task<AddressDto?> GetUserAddressByIdAsync(ulong userId, ulong addressId);

    /// <summary>
    /// Creates and saves a new delivery address for the user.
    /// If marked as default or if it's the user's first address, sets it as the default.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="model">The address creation details.</param>
    /// <returns>The created address.</returns>
    public Task<AddressDto> CreateAddressAsync(ulong userId, AddressCreateModel model);

    /// <summary>
    /// Updates an existing address owned by the user.
    /// </summary>
    public Task<AddressDto?> UpdateAddressAsync(ulong userId, ulong addressId, AddressCreateModel model);

    /// <summary>
    /// Sets an address as the user's default delivery address, unsetting any previous default.
    /// </summary>
    public Task<bool> SetDefaultAddressAsync(ulong userId, ulong addressId);

    /// <summary>
    /// Soft-deletes an address owned by the user.
    /// </summary>
    public Task<bool> DeleteAddressAsync(ulong userId, ulong addressId);
}
