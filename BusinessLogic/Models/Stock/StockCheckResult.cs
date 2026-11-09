namespace BusinessLogic.Models.Stock;

/// <summary>
/// Status of an individual product's stock availability check.
/// </summary>
public record StockCheckItem(
    ulong ProductId,
    string ProductName,
    uint RequestedQuantity,
    uint AvailableStock,
    bool IsAvailable);

/// <summary>
/// Result of a bulk stock availability check.
/// </summary>
public record StockCheckResult(
    bool IsAllAvailable,
    IReadOnlyList<StockCheckItem> Items);
