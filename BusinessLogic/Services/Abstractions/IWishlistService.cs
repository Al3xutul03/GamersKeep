using BusinessLogic.Models.Wishlist;

namespace BusinessLogic.Services.Abstractions;

public interface IWishlistService
{
    Task<IReadOnlyList<WishlistItemDto>> GetUserWishlistAsync(ulong userId);

    Task<HashSet<ulong>> GetProductIdsAsync(ulong userId);

    Task<bool> ToggleAsync(ulong userId, ulong productId);
}