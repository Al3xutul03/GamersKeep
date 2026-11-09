using BusinessLogic.Models.Order;
using Repository.Enums.Types;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// Service managing customer order history, details, and order state transitions.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Retrieves all orders placed by the user, newest first, for the order history view.
    /// Displays date, products, total and status.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A list of order listing DTOs.</returns>
    public Task<IReadOnlyList<OrderListingDto>> GetUserOrdersAsync(ulong userId);

    /// <summary>
    /// Retrieves full order details by order number for a specific user (or admin).
    /// </summary>
    /// <param name="userId">The ID of the requesting user.</param>
    /// <param name="orderNumber">The unique order number.</param>
    /// <param name="isAdmin">Whether the requesting user is an administrator.</param>
    /// <returns>The order detail DTO, or <c>null</c> if not found or unauthorized.</returns>
    public Task<OrderDetailDto?> GetOrderByNumberAsync(ulong userId, string orderNumber, bool isAdmin = false);

    /// <summary>
    /// Retrieves full order details by order ID.
    /// </summary>
    /// <param name="orderId">The ID of the order.</param>
    /// <returns>The order detail DTO, or <c>null</c> if not found.</returns>
    public Task<OrderDetailDto?> GetOrderByIdAsync(ulong orderId);

    /// <summary>
    /// Cancels an order. If stock was deducted, restores it atomically.
    /// </summary>
    /// <param name="userId">The ID of the requesting user.</param>
    /// <param name="orderId">The ID of the order to cancel.</param>
    /// <param name="isAdmin">Whether the requesting user is an administrator.</param>
    /// <returns><c>true</c> if successfully cancelled; otherwise <c>false</c>.</returns>
    public Task<bool> CancelOrderAsync(ulong userId, ulong orderId, bool isAdmin = false);
}
