using BusinessLogic.Models.Checkout;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// Service coordinating the multi-step checkout workflow:
/// address selection, delivery method selection, summary calculation, and order placement.
/// </summary>
public interface ICheckoutService
{
    /// <summary>
    /// Computes the complete checkout summary including cart items, address, delivery method,
    /// and server-side verified totals.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="addressId">The selected delivery address ID.</param>
    /// <param name="deliveryMethodId">The selected delivery method ID.</param>
    /// <returns>The checkout order summary.</returns>
    public Task<CheckoutSummaryDto> GetCheckoutSummaryAsync(ulong userId, ulong addressId, ulong deliveryMethodId);

    /// <summary>
    /// Places an order from the user's active shopping cart in <c>Unpaid</c> status.
    /// Snapshots all items, product prices, delivery method, and shipping address.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="addressId">The selected delivery address ID.</param>
    /// <param name="deliveryMethodId">The selected delivery method ID.</param>
    /// <returns>The created order ID and public order number.</returns>
    public Task<CreateOrderResult> PlaceOrderAsync(ulong userId, ulong addressId, ulong deliveryMethodId);
}
