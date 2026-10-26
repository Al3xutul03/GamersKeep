using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class CheckoutController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var model = new CheckoutViewModel
        {
            Cart = new CartViewModel()
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult ProcessOrder(CheckoutViewModel model)
    {
        var orderNumber = "GK-" + Random.Shared.Next(100000, 999999);

        return RedirectToAction("Confirmation", new { orderNumber, email = model.Email, total = model.TotalWithShipping });
    }

    [HttpGet]
    public IActionResult Confirmation(string orderNumber, string email, decimal total)
    {
        var model = new OrderConfirmationViewModel
        {
            OrderNumber = orderNumber,
            CustomerEmail = email,
            TotalAmount = total
        };
        return View(model);
    }
}