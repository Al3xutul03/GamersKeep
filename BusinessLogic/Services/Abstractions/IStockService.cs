using BusinessLogic.Models.Stock;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// Service managing product stock reduction upon payment confirmation,
/// stock restoration upon order cancellation, and availability checks.
/// </summary>
public interface IStockService
{
    /// <summary>
    /// Atomically decreases stock for all items in an order when payment is confirmed.
    /// Idempotent: if stock was already deducted for this order, does nothing.
    /// Throws <see cref="Exceptions.InsufficientStockException"/> if any product does not have enough stock.
    /// </summary>
    /// <param name="orderId">The ID of the order.</param>
    public Task DecreaseStockForOrderAsync(ulong orderId);

    /// <summary>
    /// Atomically restores stock for all items in an order when the order is cancelled.
    /// Idempotent: if stock was never deducted or was already restored, does nothing.
    /// </summary>
    /// <param name="orderId">The ID of the order.</param>
    public Task RestoreStockForOrderAsync(ulong orderId);

    /// <summary>
    /// Checks current stock availability for a list of products and requested quantities.
    /// </summary>
    /// <param name="items">Pairs of product ID and requested quantity.</param>
    /// <returns>A detailed availability result.</returns>
    public Task<StockCheckResult> CheckStockAvailabilityAsync(IEnumerable<(ulong ProductId, uint Quantity)> items);

    /// <summary>
    /// Checks whether a specific product has at least the requested quantity available in stock.
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="requestedQuantity">The requested quantity.</param>
    /// <returns><c>true</c> if stock is available; otherwise <c>false</c>.</returns>
    public Task<bool> IsQuantityAvailableAsync(ulong productId, uint requestedQuantity);
}
