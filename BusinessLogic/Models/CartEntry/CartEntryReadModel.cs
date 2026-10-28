namespace BusinessLogic.Models.CartEntry;

/// <summary>
/// Data transfer object for cart entry read information.
/// </summary>
public record CartEntryReadModel(
    ulong UserId,
    ulong ProductId,
    uint Quantity,
    DateTime CreatedAt,
    DateTime UpdatedAt);
