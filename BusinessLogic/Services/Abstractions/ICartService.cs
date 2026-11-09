using BusinessLogic.Models.CartEntry;
using BusinessLogic.Models.Stock;
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
    /// Rejects the addition if total quantity exceeds available stock.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="quantity">The quantity to add.</param>
    public Task AddOrUpdateItemAsync(ulong userId, ulong productId, uint quantity = 1);

    /// <summary>
    /// Sets the exact quantity for a product in the user's cart.
    /// If quantity is 0, removes the item from the cart.
    /// If quantity exceeds available stock, throws <see cref="Exceptions.InsufficientStockException"/>.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="quantity">The new quantity.</param>
    public Task SetQuantityAsync(ulong userId, ulong productId, uint quantity);

    /// <summary>
    /// Removes all entries from the user's shopping cart (e.g. after successful order payment).
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    public Task ClearCartAsync(ulong userId);

    /// <summary>
    /// Validates all items in the user's cart against live product stock.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A stock check result indicating whether all items are in stock.</returns>
    public Task<StockCheckResult> ValidateCartAsync(ulong userId);
}
