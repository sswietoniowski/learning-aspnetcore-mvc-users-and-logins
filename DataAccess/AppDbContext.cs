using learning_aspnetcore_mvc_users_and_logins.Configurations.Options;
using learning_aspnetcore_mvc_users_and_logins.Entities;
using learning_aspnetcore_mvc_users_and_logins.Services;
using Microsoft.EntityFrameworkCore;

namespace learning_aspnetcore_mvc_users_and_logins.DataAccess;

public class AppDbContext : DbContext
{
    private readonly IPasswordHasher _passwordHasher;

    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options, IPasswordHasher? passwordHasher) : base(options)
    {
        _passwordHasher = passwordHasher ?? new PasswordHasher();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // enums are stored as strings in the database, based on this article:
        // https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations
        modelBuilder
            .Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        var salt1 = _passwordHasher.GenerateSalt();
        var salt2 = _passwordHasher.GenerateSalt();

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                UserName = "jdoe",
                PasswordHash = _passwordHasher.HashPassword("P@ssw0rd", salt1),
                PasswordSalt = salt1,
                Role = Role.Customer
            },
            new User
            {
                Id = 2,
                UserName = "afox",
                PasswordHash = _passwordHasher.HashPassword("P@ssw0rd", salt2),
                PasswordSalt = salt2,
                Role = Role.Employee
            });

        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Id = 1,
                Product = "Product 1",
                Quantity = 1,
                Price = 1.0m,
                UserId = 1
            },
            new Order
            {
                Id = 2,
                Product = "Product 2",
                Quantity = 2,
                Price = 2.0m,
                UserId = 1
            });
    }
}