namespace learning_aspnetcore_mvc_users_and_logins.Services;

public interface IPasswordHasher
{
    string HashPassword(string password, string salt);
    string GenerateSalt();
}