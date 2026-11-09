using System.Security.Cryptography;
using AutoMapper;
using BusinessLogic.Exceptions;
using BusinessLogic.Models.Address;
using BusinessLogic.Models.Checkout;
using BusinessLogic.Models.DeliveryMethod;
using BusinessLogic.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Enums.Types;
using Repository.Repositories.Abstractions;

namespace BusinessLogic.Services;

/// <summary>
/// Service coordinating the multi-step checkout workflow:
/// address selection, delivery method selection, summary calculation, and order placement.
/// </summary>
public class CheckoutService(
    ICartService cartService,
    IAddressRepository addressRepository,
    IDeliveryMethodRepository deliveryMethodRepository,
    IOrderRepository orderRepository,
    IMapper mapper,
    ILogger<CheckoutService> logger) : ICheckoutService
{
    private readonly ICartService _cartService = cartService;
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly IDeliveryMethodRepository _deliveryMethodRepository = deliveryMethodRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<CheckoutService> _logger = logger;

    /// <inheritdoc />
    public async Task<CheckoutSummaryDto> GetCheckoutSummaryAsync(ulong userId, ulong addressId, ulong deliveryMethodId)
    {
        var cartEntries = await _cartService.GetUserCartAsync(userId);
        if (cartEntries.Count == 0)
        {
            throw new InvalidOperationException("Coșul de cumpărături este gol.");
        }

        var address = await _addressRepository.GetUserAddressAsync(userId, addressId)
            ?? throw new KeyNotFoundException("Adresa de livrare selectată nu a fost găsită.");

        var deliveryMethod = await _deliveryMethodRepository.GetByIdAsync(deliveryMethodId, IncludeBehaviour.NoInclude);
        if (deliveryMethod == null || !deliveryMethod.IsActive)
        {
            throw new InvalidOperationException("Metoda de livrare selectată nu este activă sau validă.");
        }

        var items = cartEntries
            .Where(e => e.Product != null)
            .Select(e => new CheckoutSummaryItemDto(
                e.ProductId,
                e.Product!.Name,
                e.Product.FirstImageUrl,
                e.Product.Price,
                e.Quantity,
                e.Product.Price * e.Quantity))
            .ToList();

        var subtotal = items.Sum(i => i.LineTotal);
        var deliveryCost = deliveryMethod.Price;
        var grandTotal = subtotal + deliveryCost;

        return new CheckoutSummaryDto(
            items,
            _mapper.Map<AddressDto>(address),
            _mapper.Map<DeliveryMethodDto>(deliveryMethod),
            subtotal,
            deliveryCost,
            grandTotal);
    }

    /// <inheritdoc />
    public async Task<CreateOrderResult> PlaceOrderAsync(ulong userId, ulong addressId, ulong deliveryMethodId)
    {
        var cartEntries = await _cartService.GetUserCartAsync(userId);
        if (cartEntries.Count == 0)
        {
            throw new InvalidOperationException("Coșul de cumpărături este gol.");
        }

        // Validate stock availability before creating the order
        var stockCheck = await _cartService.ValidateCartAsync(userId);
        if (!stockCheck.IsAllAvailable)
        {
            var failing = stockCheck.Items.First(i => !i.IsAvailable);
            throw new InsufficientStockException(
                failing.ProductId,
                failing.RequestedQuantity,
                failing.AvailableStock,
                failing.ProductName);
        }

        var address = await _addressRepository.GetUserAddressAsync(userId, addressId)
            ?? throw new KeyNotFoundException("Adresa de livrare selectată nu a fost găsită.");

        var deliveryMethod = await _deliveryMethodRepository.GetByIdAsync(deliveryMethodId, IncludeBehaviour.NoInclude);
        if (deliveryMethod == null || !deliveryMethod.IsActive)
        {
            throw new InvalidOperationException("Metoda de livrare selectată nu este activă sau validă.");
        }

        var validCartItems = cartEntries.Where(e => e.Product != null).ToList();
        var subtotal = validCartItems.Sum(e => e.Product!.Price * e.Quantity);
        var deliveryCost = deliveryMethod.Price;
        var grandTotal = subtotal + deliveryCost;

        var orderNumber = await GenerateUniqueOrderNumberAsync();
        var now = DateTime.UtcNow;

        var order = new Order
        {
            OrderNumber = orderNumber,
            UserId = userId,
            Status = OrderStatusType.Unpaid,
            DeliveryMethodId = deliveryMethod.Id,
            DeliveryMethodName = deliveryMethod.Name,
            DeliveryCost = deliveryCost,
            Subtotal = subtotal,
            Total = grandTotal,
            Currency = "RON",
            ShippingFullName = address.FullName,
            ShippingPhone = address.Phone,
            ShippingStreet = address.Street,
            ShippingCity = address.City,
            ShippingCounty = address.County,
            ShippingPostalCode = address.PostalCode,
            StockDeducted = false,
            CreatedAt = now,
            UpdatedAt = now
        };

        foreach (var entry in validCartItems)
        {
            order.Items.Add(new OrderItem
            {
                ProductId = entry.ProductId,
                ProductName = entry.Product!.Name,
                UnitPrice = entry.Product.Price,
                Quantity = entry.Quantity
            });
        }

        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveAsync();

        _logger.LogInformation("Order {OrderNumber} placed in Unpaid status for user {UserId}. Total: {Total} RON", orderNumber, userId, grandTotal);

        return new CreateOrderResult(order.Id, order.OrderNumber, order.Total);
    }

    private async Task<string> GenerateUniqueOrderNumberAsync()
    {
        const string alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        var datePrefix = DateTime.UtcNow.ToString("yyyyMMdd");

        for (int i = 0; i < 10; i++)
        {
            var randomCode = RandomNumberGenerator.GetString(alphabet, 6);
            var candidate = $"GK-{datePrefix}-{randomCode}";

            if (!await _orderRepository.OrderNumberExistsAsync(candidate))
            {
                return candidate;
            }
        }

        return $"GK-{datePrefix}-{Guid.NewGuid().ToString("N")[..6].ToUpperInvariant()}";
    }
}
