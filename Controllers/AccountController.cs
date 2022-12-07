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

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
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

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _accountManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}