namespace BusinessLogic.Models.CartEntry;

/// <summary>
/// Data transfer object for cart entry create information.
/// </summary>
public record CartEntryCreateModel(
    ulong UserId,
    ulong ProductId,
    uint Quantity);
