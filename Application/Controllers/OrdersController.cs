using BusinessLogic.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Enums.Types;
using System.Security.Claims;

namespace Application.Controllers;

[Authorize]
[Route("orders")]
public class OrdersController(IOrderService orderService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        if (!TryGetUserId(out var userId))
            return RedirectToAction("Login", "Account");

        var orders = await orderService.GetUserOrdersAsync(userId);
        return View(orders);
    }

    [HttpGet("{orderNumber}")]
    public async Task<IActionResult> Details(string orderNumber)
    {
        if (!TryGetUserId(out var userId))
            return RedirectToAction("Login", "Account");

        var order = await orderService.GetOrderByNumberAsync(userId, orderNumber);
        if (order == null)
            return NotFound();

        return View(order);
    }

    [HttpPost("{orderNumber}/cancel"), ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(string orderNumber)
    {
        if (!TryGetUserId(out var userId))
            return RedirectToAction("Login", "Account");

        var order = await orderService.GetOrderByNumberAsync(userId, orderNumber);
        if (order == null)
            return NotFound();

        if (order.Status != OrderStatusType.Unpaid)
        {
            TempData["OrderError"] = "Doar comenzile neplătite pot fi anulate din cont.";
            return RedirectToAction(nameof(Details), new { orderNumber });
        }

        var cancelled = await orderService.CancelOrderAsync(userId, order.Id);
        if (cancelled)
            TempData["OrderMessage"] = "Comanda a fost anulată.";
        else
            TempData["OrderError"] = "Comanda nu a putut fi anulată.";

        return RedirectToAction(nameof(Details), new { orderNumber });
    }

    private bool TryGetUserId(out ulong userId)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return ulong.TryParse(claim, out userId);
    }
}