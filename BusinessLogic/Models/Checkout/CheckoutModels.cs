using BusinessLogic.Models.Address;
using BusinessLogic.Models.DeliveryMethod;

namespace BusinessLogic.Models.Checkout;

/// <summary>
/// A line item in the checkout order summary.
/// </summary>
public record CheckoutSummaryItemDto(
    ulong ProductId,
    string ProductName,
    string? ImageUrl,
    decimal UnitPrice,
    uint Quantity,
    decimal LineTotal);

/// <summary>
/// Complete checkout order summary before payment.
/// </summary>
public record CheckoutSummaryDto(
    IReadOnlyList<CheckoutSummaryItemDto> Items,
    AddressDto ShippingAddress,
    DeliveryMethodDto DeliveryMethod,
    decimal Subtotal,
    decimal DeliveryCost,
    decimal GrandTotal);

/// <summary>
/// Request to place an order from the user's active cart.
/// </summary>
public record CreateOrderRequest(
    ulong AddressId,
    ulong DeliveryMethodId);

/// <summary>
/// Result of placing an order.
/// </summary>
public record CreateOrderResult(
    ulong OrderId,
    string OrderNumber,
    decimal Total);
