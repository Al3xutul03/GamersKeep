namespace Application.Models;

public record WishlistButtonModel(ulong ProductId, bool Active, bool ShowLabel = false);