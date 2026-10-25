using Application.Models;
using BusinessLogic.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class CartController(IProductService productService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var cart = new CartViewModel();
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(ulong productId, int quantity = 1)
    {
        var product = await productService.GetByIdAsync(productId);
        if (product == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveFromCart(ulong productId)
    {
        return RedirectToAction("Index");
    }
}