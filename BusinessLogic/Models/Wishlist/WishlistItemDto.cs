namespace BusinessLogic.Models.Wishlist;

public record WishlistItemDto(
    ulong ProductId,
    string Name,
    decimal Price,
    uint Stock,
    string ImageUrl,
    DateTime AddedAt);