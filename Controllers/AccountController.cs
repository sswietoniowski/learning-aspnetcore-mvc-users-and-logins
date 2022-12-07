using Microsoft.AspNetCore.Mvc;

namespace learning_aspnetcore_mvc_users_and_logins.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
