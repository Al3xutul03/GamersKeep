using Application.Models;
using BusinessLogic.Exceptions;
using BusinessLogic.Models.Address;
using BusinessLogic.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Enums.Types;
using Repository.Repositories.Abstractions;
using System.Security.Claims;

namespace Application.Controllers;

[Authorize]
public class CheckoutController(
    ICartService cartService,
    IAddressService addressService,
    IDeliveryMethodService deliveryMethodService,
    ICheckoutService checkoutService,
    IPaymentGateway paymentGateway,
    IOrderRepository orderRepository,
    ILogger<CheckoutController> logger) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!TryGetUserId(out var userId))
            return RedirectToAction("Login", "Account");

        var model = await BuildModelAsync(userId, new CheckoutViewModel());
        if (model.Cart.Items.Count == 0)
            return RedirectToAction("Index", "Cart");

        model.SelectedAddressId = model.SavedAddresses.FirstOrDefault()?.Id ?? 0;
        model.DeliveryMethodId = model.DeliveryMethods.FirstOrDefault()?.Id ?? 0;

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcessOrder(CheckoutViewModel posted)
    {
        if (!TryGetUserId(out var userId))
            return RedirectToAction("Login", "Account");

        var model = await BuildModelAsync(userId, posted);
        if (model.Cart.Items.Count == 0)
            return RedirectToAction("Index", "Cart");

        if (model.SelectedAddressId != 0 && model.SavedAddresses.All(a => a.Id != model.SelectedAddressId))
            ModelState.AddModelError(nameof(model.SelectedAddressId), "Adresa selectată nu este validă.");

        if (model.DeliveryMethodId != 0 && model.DeliveryMethods.All(d => d.Id != model.DeliveryMethodId))
            ModelState.AddModelError(nameof(model.DeliveryMethodId), "Metoda de livrare selectată nu este disponibilă.");

        if (!ModelState.IsValid)
            return View("Index", model);

        try
        {
            var addressId = model.SelectedAddressId;
            if (addressId == 0)
            {
                var created = await addressService.CreateAddressAsync(userId, new AddressCreateModel(
                    null,
                    model.FullName.Trim(),
                    model.Phone.Trim(),
                    model.Street.Trim(),
                    model.City.Trim(),
                    model.County.Trim(),
                    model.PostalCode.Trim()));
                addressId = created.Id;
            }

            var result = await checkoutService.PlaceOrderAsync(userId, addressId, model.DeliveryMethodId);

            var order = await orderRepository.GetWithItemsAsync(result.OrderId)
                ?? throw new InvalidOperationException("Comanda tocmai creată nu a putut fi găsită.");

            var customerEmail = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            var session = await paymentGateway.CreateCheckoutSessionAsync(order, customerEmail);

            return Redirect(session.CheckoutUrl);
        }
        catch (InsufficientStockException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Checkout failed for user {UserId}.", userId);
            ModelState.AddModelError(string.Empty, "Plata nu a putut fi inițiată. Încearcă din nou.");
        }

        return View("Index", model);
    }

    [HttpGet]
    public async Task<IActionResult> Confirmation([FromQuery(Name = "session_id")] string? sessionId)
    {
        if (!TryGetUserId(out var userId))
            return RedirectToAction("Login", "Account");

        if (string.IsNullOrWhiteSpace(sessionId))
            return RedirectToAction("Index", "Catalog");

        var order = await orderRepository.GetByStripeSessionIdAsync(sessionId);
        if (order == null || order.UserId != userId)
            return NotFound();

        var model = new OrderConfirmationViewModel
        {
            OrderNumber = order.OrderNumber,
            CustomerEmail = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
            DeliveryMethodName = order.DeliveryMethodName,
            ShippingAddress = $"{order.ShippingFullName}, {order.ShippingPhone} — {order.ShippingStreet}, {order.ShippingCity}, {order.ShippingCounty}, {order.ShippingPostalCode}",
            Subtotal = order.Subtotal,
            DeliveryCost = order.DeliveryCost,
            TotalAmount = order.Total,
            IsPaid = order.Status == OrderStatusType.Paid,
            Items = order.Items.Select(i => new OrderConfirmationItemViewModel
            {
                ProductName = i.ProductName,
                Quantity = (int)i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Cancel(ulong orderId)
    {
        TempData["CheckoutError"] = "Plata nu a fost finalizată. Coșul tău a fost păstrat, poți încerca din nou.";
        return RedirectToAction("Index", "Cart");
    }

    private async Task<CheckoutViewModel> BuildModelAsync(ulong userId, CheckoutViewModel model)
    {
        var entries = await cartService.GetUserCartAsync(userId);

        model.Cart = new CartViewModel
        {
            Items = entries
                .Where(e => e.Product != null)
                .Select(e => new CartItemViewModel
                {
                    ProductId = e.ProductId,
                    ProductName = e.Product!.Name,
                    ImageUrl = e.Product.FirstImageUrl,
                    UnitPrice = e.Product.Price,
                    Quantity = (int)e.Quantity
                })
                .ToList()
        };

        model.SavedAddresses = await addressService.GetUserAddressesAsync(userId);
        model.DeliveryMethods = await deliveryMethodService.GetActiveDeliveryMethodsAsync();

        return model;
    }

    private bool TryGetUserId(out ulong userId)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return ulong.TryParse(claim, out userId);
    }
}