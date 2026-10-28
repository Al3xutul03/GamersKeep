namespace BusinessLogic.Models.CartEntry;

/// <summary>
/// Data transfer object for cart entry update information.
/// </summary>
public record CartEntryUpdateModel(
    ulong UserId,
    ulong ProductId,
    uint Quantity);
