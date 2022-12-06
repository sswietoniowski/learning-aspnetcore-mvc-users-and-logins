using learning_aspnetcore_mvc_users_and_logins.Entities;
using Microsoft.EntityFrameworkCore;

namespace learning_aspnetcore_mvc_users_and_logins.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<Entities.Order> Orders { get; set; } = default!;
    public DbSet<Entities.User> Users { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Login = "jkowalski",
                Password = "ABC",
                Salt = "DEF",
                Role = Role.Customer
            },
            new User
            {
                Id = 2,
                Login = "anowak",
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