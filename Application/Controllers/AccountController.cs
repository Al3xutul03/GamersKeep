using Application.Models;
using BusinessLogic.Models.Login;
using BusinessLogic.Models.User;
using BusinessLogic.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Controllers;

public class AccountController(IAuthService authService) : Controller
{
    [HttpGet, AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Home");
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var response = await authService.Login(new LoginModel(vm.Username, vm.Password));
        if (response is null)
        {
            ModelState.AddModelError(string.Empty, "Username or password is incorrect.");
            vm.Password = "";
            return View(vm);
        }

        await SignInAsync(response, vm.RememberMe);
        return LocalRedirect(Url.IsLocalUrl(vm.ReturnUrl) ? vm.ReturnUrl! : "/");
    }

    [HttpGet, AllowAnonymous]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Home");
        return View(new RegisterViewModel());
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var response = await authService.Register(new UserCreateModel(vm.Name, vm.Email, vm.Password));
        if (response is null)
        {
            ModelState.AddModelError(nameof(vm.Email), "An account with this email already exists.");
            return View(vm);
        }

        await SignInAsync(response, isPersistent: false);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost, Authorize, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    /// <summary>
    /// Builds the login cookie from the claims inside the JWT issued by <see cref="IAuthService"/>.
    /// </summary>
    private Task SignInAsync(LoginResponseModel response, bool isPersistent)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(response.Token);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, jwt.Claims.First(c => c.Type == "id").Value),
            new(ClaimTypes.Name, jwt.Claims.First(c => c.Type == "name").Value),
            new(ClaimTypes.Email, jwt.Claims.First(c => c.Type == "email").Value),
        };

        if (jwt.Claims.FirstOrDefault(c => c.Type == "isAdmin")?.Value == "true")
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

        var principal = new ClaimsPrincipal(
            new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

        return HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties { IsPersistent = isPersistent });
    }
}