using BusinessLogic.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Application.Controllers;

[Authorize]
public class WishlistController(IWishlistService wishlistService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!TryGetUserId(out var userId))
            return RedirectToAction("Login", "Account");

        var items = await wishlistService.GetUserWishlistAsync(userId);
        return View(items);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(ulong productId, string? returnUrl)
    {
        if (!TryGetUserId(out var userId))
            return RedirectToAction("Login", "Account");

        try
        {
            await wishlistService.ToggleAsync(userId, productId);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);

        return RedirectToAction(nameof(Index));
    }

    private bool TryGetUserId(out ulong userId)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return ulong.TryParse(claim, out userId);
    }
}