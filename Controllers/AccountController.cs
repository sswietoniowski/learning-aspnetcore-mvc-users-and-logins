using learning_aspnetcore_mvc_users_and_logins.Models;
using learning_aspnetcore_mvc_users_and_logins.Services;
using Microsoft.AspNetCore.Mvc;

namespace learning_aspnetcore_mvc_users_and_logins.Controllers;

public class AccountController : Controller
{
    private readonly IAccountManager _accountManager;

    public AccountController(IAccountManager accountManager)
    {
        _accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));
    }

    public IActionResult Login(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        this.ViewData["ReturnUrl"] = returnUrl;
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm loginVm)
    {
        if (!ModelState.IsValid)
        {
            return View(loginVm);
        }

        if (!await _accountManager.PasswordSignInAsync(loginVm.UserName, loginVm.Password, loginVm.RememberMe))
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(loginVm);
        }

        var returnUrl = HttpContext.Request.Query["returnUrl"];
        if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl!);
        else
            return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _accountManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}