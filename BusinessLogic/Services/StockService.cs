using BusinessLogic.Exceptions;
using BusinessLogic.Models.Stock;
using BusinessLogic.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Abstractions;

namespace BusinessLogic.Services;

/// <summary>
/// Service managing product stock reduction upon payment confirmation,
/// stock restoration upon order cancellation, and availability checks.
/// </summary>
public class StockService(
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    ILogger<StockService> logger) : IStockService
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ILogger<StockService> _logger = logger;

    /// <inheritdoc />
    public async Task DecreaseStockForOrderAsync(ulong orderId)
    {
        var order = await _orderRepository.GetWithItemsAsync(orderId)
            ?? throw new KeyNotFoundException($"Order with ID {orderId} was not found.");

        if (order.StockDeducted)
        {
            _logger.LogInformation("Stock for order {OrderNumber} (ID: {OrderId}) was already deducted. Skipping.", order.OrderNumber, orderId);
            return;
        }

        if (order.Items.Count == 0)
        {
            _logger.LogWarning("Order {OrderNumber} has no items. Marking stock as deducted.", order.OrderNumber);
            order.StockDeducted = true;
            order.UpdatedAt = DateTime.UtcNow;
            await _orderRepository.SaveAsync();
            return;
        }

        var items = order.Items.Select(i => (i.ProductId, i.Quantity)).ToList();

        var (success, failedProductId) = await _productRepository.TryAtomicDecrementStockBatchAsync(items);

        if (!success)
        {
            string? productName = null;
            uint availableStock = 0;
            uint requestedQty = 0;

            if (failedProductId.HasValue)
            {
                var failedItem = order.Items.FirstOrDefault(i => i.ProductId == failedProductId.Value);
                productName = failedItem?.ProductName;
                requestedQty = failedItem?.Quantity ?? 0;

                var stockMap = await _productRepository.GetStockForProductsAsync([failedProductId.Value]);
                stockMap.TryGetValue(failedProductId.Value, out availableStock);
            }

            _logger.LogError(
                "Failed to decrease stock for order {OrderNumber}. Product {ProductId} ('{ProductName}') has insufficient stock (Requested: {Req}, Available: {Avail}).",
                order.OrderNumber, failedProductId, productName, requestedQty, availableStock);

            throw new InsufficientStockException(
                failedProductId ?? 0,
                requestedQty,
                availableStock,
                productName);
        }

        order.StockDeducted = true;
        order.UpdatedAt = DateTime.UtcNow;
        await _orderRepository.SaveAsync();

        _logger.LogInformation("Successfully decreased stock for order {OrderNumber} ({Count} items).", order.OrderNumber, items.Count);
    }

    /// <inheritdoc />
    public async Task RestoreStockForOrderAsync(ulong orderId)
    {
        var order = await _orderRepository.GetWithItemsAsync(orderId)
            ?? throw new KeyNotFoundException($"Order with ID {orderId} was not found.");

        if (!order.StockDeducted)
        {
            _logger.LogInformation(
                "Stock for order {OrderNumber} (ID: {OrderId}) was not deducted (or was already restored). Skipping restoration.",
                order.OrderNumber, orderId);
            return;
        }

        if (order.Items.Count > 0)
        {
            var items = order.Items.Select(i => (i.ProductId, i.Quantity)).ToList();
            await _productRepository.AtomicIncrementStockBatchAsync(items);
        }

        order.StockDeducted = false;
        order.UpdatedAt = DateTime.UtcNow;
        await _orderRepository.SaveAsync();

        _logger.LogInformation("Successfully restored stock for cancelled order {OrderNumber}.", order.OrderNumber);
    }

    /// <inheritdoc />
    public async Task<StockCheckResult> CheckStockAvailabilityAsync(IEnumerable<(ulong ProductId, uint Quantity)> items)
    {
        var itemList = items.ToList();
        if (itemList.Count == 0)
        {
            return new StockCheckResult(true, Array.Empty<StockCheckItem>());
        }

        var productIds = itemList.Select(i => i.ProductId).Distinct().ToList();
        var stockMap = await _productRepository.GetStockForProductsAsync(productIds);

        var resultItems = new List<StockCheckItem>();
        bool isAllAvailable = true;

        foreach (var (productId, requestedQuantity) in itemList)
        {
            stockMap.TryGetValue(productId, out var availableStock);
            bool available = availableStock >= requestedQuantity;
            if (!available)
            {
                isAllAvailable = false;
            }

            var product = await _productRepository.GetByIdAsync(productId, Repository.Enums.Behaviours.IncludeBehaviour.NoInclude);
            var productName = product?.Name ?? $"Produs #{productId}";

            resultItems.Add(new StockCheckItem(
                productId,
                productName,
                requestedQuantity,
                availableStock,
                available));
        }

        return new StockCheckResult(isAllAvailable, resultItems);
    }

    /// <inheritdoc />
    public async Task<bool> IsQuantityAvailableAsync(ulong productId, uint requestedQuantity)
    {
        var stockMap = await _productRepository.GetStockForProductsAsync([productId]);
        return stockMap.TryGetValue(productId, out var stock) && stock >= requestedQuantity;
    }
}
