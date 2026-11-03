using BusinessLogic.Models.CartEntry;
using BusinessLogic.Services.Generic;
using Repository.Entity;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// Cart service interface for managing user shopping cart operations.
/// </summary>
public interface ICartService
    : IBaseService<(ulong UserId, ulong ProductId), CartEntry, CartEntryReadModel, CartEntryCreateModel, CartEntryUpdateModel>
{
    /// <summary>
    /// Retrieves all cart entries for a specific user, including product details.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A collection of cart entries for the user.</returns>
    public Task<ICollection<CartEntryReadModel>> GetUserCartAsync(ulong userId);

    /// <summary>
    /// Adds a product to the user's cart, or increments the quantity if it already exists.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="quantity">The quantity to add.</param>
    public Task AddOrUpdateItemAsync(ulong userId, ulong productId, uint quantity = 1);
}
