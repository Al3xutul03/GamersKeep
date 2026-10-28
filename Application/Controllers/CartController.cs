using Application.Models;
using BusinessLogic.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Application.Controllers;

[Authorize]
public class CartController(ICartService cartService, IProductService productService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!TryGetUserId(out var userId))
            return RedirectToAction("Login", "Account");

        var entries = await cartService.GetUserCartAsync(userId);

        var cart = new CartViewModel
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

        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(ulong productId, int quantity = 1)
    {
        if (!TryGetUserId(out var userId))
            return RedirectToAction("Login", "Account");

        var product = await productService.GetByIdAsync(productId);
        if (product == null)
        {
            return NotFound();
        }

        uint safeQuantity = quantity < 1 ? 1u : (uint)quantity;
        await cartService.AddOrUpdateItemAsync(userId, productId, safeQuantity);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(ulong productId)
    {
        if (!TryGetUserId(out var userId))
            return RedirectToAction("Login", "Account");

        try
        {
            await cartService.DeleteAsync((userId, productId));
        }
        catch
        {
            // Ignore if entry was already removed
        }

        return RedirectToAction("Index");
    }

    private bool TryGetUserId(out ulong userId)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return ulong.TryParse(claim, out userId);
    }
}
