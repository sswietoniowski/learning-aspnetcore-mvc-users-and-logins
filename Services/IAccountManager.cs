﻿namespace learning_aspnetcore_mvc_users_and_logins.Services;

public interface IAccountManager
{
    Task<bool> PasswordSignInAsync(string userName, string password, bool rememberMe);
    Task SignOutAsync();
    Task<string?> GetUserName();
}