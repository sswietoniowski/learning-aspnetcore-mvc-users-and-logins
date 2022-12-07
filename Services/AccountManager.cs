using learning_aspnetcore_mvc_users_and_logins.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace learning_aspnetcore_mvc_users_and_logins.Services;

public class AccountManager : IAccountManager
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountManager(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<bool> PasswordSignInAsync(string userName, string password, bool rememberMe)
    {
        // TODO: passwords should not be stored as a plain text (one of many reasons why we should reinvent the wheel but use Identity Framework instead)

        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);

        if (user is null)
        {
            return false;
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("role", user.Role.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        if (_httpContextAccessor?.HttpContext is null)
        {
            throw new InvalidOperationException("HttpContext is null");
        }

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties { IsPersistent = rememberMe });

        return true;
    }

    public async Task SignOutAsync()
    {
        if (_httpContextAccessor?.HttpContext is null)
        {
            throw new InvalidOperationException("HttpContext is null");
        }

        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task<bool> IsSignedIn(ClaimsPrincipal userPrincipal)
    {
        var userName = userPrincipal?.Identity?.Name;
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

        if (user is null)
        {
            return false;
        }

        return true;
    }

    public async Task<string?> GetUserName(ClaimsPrincipal userPrincipal)
    {
        var userName = userPrincipal?.Identity?.Name;
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

        if (user is null)
        {
            return null;
        }

        return user.UserName;
    }
}