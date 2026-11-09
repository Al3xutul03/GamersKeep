using Repository.Enums.Types;

namespace BusinessLogic.Models.Order;

/// <summary>
/// Data transfer object for a line item within an order.
/// </summary>
public record OrderItemDto(
    ulong ProductId,
    string ProductName,
    decimal UnitPrice,
    uint Quantity,
    decimal TotalPrice);

/// <summary>
/// Data transfer object for order history listings.
/// </summary>
public record OrderListingDto(
    ulong Id,
    string OrderNumber,
    OrderStatusType Status,
    DateTime CreatedAt,
    decimal Subtotal,
    decimal DeliveryCost,
    decimal Total,
    int ItemCount,
    IReadOnlyList<OrderItemDto> Items);

/// <summary>
/// Data transfer object for full order details and receipts.
/// </summary>
public record OrderDetailDto(
    ulong Id,
    string OrderNumber,
    OrderStatusType Status,
    DateTime CreatedAt,
    DateTime? PaidAt,
    DateTime? CancelledAt,
    string ShippingFullName,
    string ShippingPhone,
    string ShippingStreet,
    string ShippingCity,
    string ShippingCounty,
    string ShippingPostalCode,
    string DeliveryMethodName,
    decimal DeliveryCost,
    decimal Subtotal,
    decimal Total,
    IReadOnlyList<OrderItemDto> Items);
