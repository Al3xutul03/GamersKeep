using Repository.Entity;
using Repository.Repositories.Generic;

namespace Repository.Repositories.Abstractions;

/// <summary>
/// Order repository interface for customer orders.
/// </summary>
public interface IOrderRepository : IBaseRepository<ulong, Order>
{
    /// <summary>
    /// Read all orders of a user, newest first, including their items.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The user's order history.</returns>
    public Task<IEnumerable<Order>> GetByUserIdAsync(ulong userId);

    /// <summary>
    /// Read an order by its public order number, including its items,
    /// only if it belongs to the given user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="orderNumber">The public order number.</param>
    /// <returns>The order, or <c>null</c> if not found or not owned by the user.</returns>
    public Task<Order?> GetUserOrderByNumberAsync(ulong userId, string orderNumber);

    /// <summary>
    /// Read an order by its Stripe Checkout Session id, including its items.
    /// Used by the Stripe webhook.
    /// </summary>
    /// <param name="sessionId">The Stripe Checkout Session id.</param>
    /// <returns>The order, or <c>null</c> if no order matches.</returns>
    public Task<Order?> GetByStripeSessionIdAsync(string sessionId);

    /// <summary>
    /// Read an order by its id, including its items and the customer.
    /// </summary>
    /// <param name="orderId">The ID of the order.</param>
    /// <returns>The order, or <c>null</c> if not found.</returns>
    public Task<Order?> GetWithDetailsAsync(ulong orderId);

    /// <summary>
    /// Check whether an order number is already in use.
    /// </summary>
    /// <param name="orderNumber">The order number to check.</param>
    /// <returns><c>true</c> if an order with this number exists.</returns>
    public Task<bool> OrderNumberExistsAsync(string orderNumber);
}
