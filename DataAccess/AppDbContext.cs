using learning_aspnetcore_mvc_users_and_logins.Entities;
using Microsoft.EntityFrameworkCore;

namespace learning_aspnetcore_mvc_users_and_logins.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
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

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Login = "jdoe",
                Password = "ABC",
                Salt = "DEF",
                Role = Role.Customer
            },
            new User
            {
                Id = 2,
                Login = "afox",
                Password = "ABC",
                Salt = "DEF",
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