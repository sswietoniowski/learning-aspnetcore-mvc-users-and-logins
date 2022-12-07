namespace learning_aspnetcore_mvc_users_and_logins.Configurations.Options;

public class PasswordHasherOptions
{
    public int Iterations { get; set; } = 10000;
    public string Pepper { get; set; } = default!;
}