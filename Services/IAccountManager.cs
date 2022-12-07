using System.Security.Claims;

namespace learning_aspnetcore_mvc_users_and_logins.Services;

public interface IAccountManager
{
    Task<bool> PasswordSignInAsync(string userName, string password, bool rememberMe);
    Task SignOutAsync();

    bool IsSignedIn(ClaimsPrincipal userPrincipal);
    string? GetUserName(ClaimsPrincipal userPrincipal);
}