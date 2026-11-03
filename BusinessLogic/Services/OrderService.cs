using AutoMapper;
using BusinessLogic.Models.Order;
using BusinessLogic.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Repository.Enums.Types;
using Repository.Repositories.Abstractions;

namespace BusinessLogic.Services;

/// <summary>
/// Service managing customer order history, details, and order state transitions.
/// </summary>
public class OrderService(
    IMapper mapper,
    IOrderRepository orderRepository,
    IStockService stockService,
    ILogger<OrderService> logger) : IOrderService
{
    private readonly IMapper _mapper = mapper;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IStockService _stockService = stockService;
    private readonly ILogger<OrderService> _logger = logger;

    /// <inheritdoc />
    public async Task<IReadOnlyList<OrderListingDto>> GetUserOrdersAsync(ulong userId)
    {
        var orders = await _orderRepository.GetByUserIdAsync(userId);
        return orders.Select(_mapper.Map<OrderListingDto>).ToList();
    }

    /// <inheritdoc />
    public async Task<OrderDetailDto?> GetOrderByNumberAsync(ulong userId, string orderNumber, bool isAdmin = false)
    {
        var order = await _orderRepository.GetUserOrderByNumberAsync(userId, orderNumber);

        if (order == null && isAdmin)
        {
            // Admins can inspect any user's order by order number
            var allOrders = await _orderRepository.GetAllAsync(
                Repository.Enums.Behaviours.IncludeBehaviour.GivenIncludes,
                q => Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Include(q, o => o.Items));

            order = allOrders.FirstOrDefault(o => o.OrderNumber == orderNumber);
        }

        return order != null ? _mapper.Map<OrderDetailDto>(order) : null;
    }

    /// <inheritdoc />
    public async Task<OrderDetailDto?> GetOrderByIdAsync(ulong orderId)
    {
        var order = await _orderRepository.GetWithDetailsAsync(orderId);
        return order != null ? _mapper.Map<OrderDetailDto>(order) : null;
    }

    /// <inheritdoc />
    public async Task<bool> CancelOrderAsync(ulong userId, ulong orderId, bool isAdmin = false)
    {
        var order = await _orderRepository.GetWithItemsAsync(orderId);
        if (order == null)
        {
            return false;
        }

        if (!isAdmin && order.UserId != userId)
        {
            _logger.LogWarning("User {UserId} attempted to cancel order {OrderId} belonging to user {OwnerId}.", userId, orderId, order.UserId);
            return false;
        }

        if (order.Status == OrderStatusType.Cancelled)
        {
            return true;
        }

        order.Status = OrderStatusType.Cancelled;
        order.CancelledAt = DateTime.UtcNow;
        order.UpdatedAt = DateTime.UtcNow;
        await _orderRepository.SaveAsync();

        // If stock had been deducted (for paid order), restore it atomically
        await _stockService.RestoreStockForOrderAsync(orderId);

        _logger.LogInformation("Order {OrderNumber} (ID: {OrderId}) was successfully cancelled.", order.OrderNumber, orderId);
        return true;
    }
}
