using learning_aspnetcore_mvc_users_and_logins.Configurations.Options;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace learning_aspnetcore_mvc_users_and_logins.Services;

public class PasswordHasher : IPasswordHasher
{
    private readonly int _iterations;
    private readonly string _pepper;

    public PasswordHasher(IOptions<PasswordHasherOptions>? options = null)
    {
        _iterations = options?.Value.Iterations ?? 0;
        _pepper = options?.Value.Pepper ?? "";
    }

    public string HashPassword(string password, string salt)
    {
        return ComputeHash(password, salt, _pepper, _iterations);
    }

    private string ComputeHash(string password, string salt, string pepper, int iterations)
    {
        if (iterations <= 0) return password;

        using var sha256 = SHA256.Create();
        var passwordSaltPepper = $"{password}{salt}{pepper}";
        var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
        var byteHash = sha256.ComputeHash(byteValue);
        var hash = Convert.ToBase64String(byteHash);

        return ComputeHash(hash, salt, pepper, iterations - 1);
    }

    public string GenerateSalt()
    {
        using var rng = RandomNumberGenerator.Create();
        var byteSalt = new byte[16];
        rng.GetBytes(byteSalt);
        var salt = Convert.ToBase64String(byteSalt);
        return salt;
    }
}