using Application.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[AllowAnonymous]
[Route("admin")]
public class AdminController : Controller
{
    private static readonly List<AdminOrderRow> Sample =
    [
        new("GK-20261120-0007", "Ana Popescu", DateTime.UtcNow.AddHours(-2), 249.90m, "Paid"),
        new("GK-20261120-0006", "Mihai Ionescu", DateTime.UtcNow.AddHours(-5), 89.00m, "Unpaid"),
        new("GK-20261119-0005", "Elena Voican", DateTime.UtcNow.AddDays(-1), 412.50m, "Shipped"),
        new("GK-20261118-0004", "Radu Marin", DateTime.UtcNow.AddDays(-2), 134.00m, "Delivered"),
        new("GK-20261118-0003", "Ioana Dima", DateTime.UtcNow.AddDays(-2), 59.90m, "Cancelled"),
        new("GK-20261117-0002", "Paul Stan", DateTime.UtcNow.AddDays(-3), 305.00m, "Paid"),
        new("GK-20261116-0001", "Maria Radu", DateTime.UtcNow.AddDays(-4), 179.00m, "Delivered")
    ];

    [HttpGet("")]
    public IActionResult Index()
    {
        var model = new AdminDashboardViewModel(3, 1124.30m, 2, 4, Sample.Take(5).ToList());
        return View(model);
    }

    [HttpGet("orders")]
    public IActionResult Orders(string? status)
    {
        var rows = string.IsNullOrWhiteSpace(status)
            ? Sample
            : Sample.Where(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
        return View(new AdminOrdersViewModel(rows, status));
    }
}