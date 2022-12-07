using learning_aspnetcore_mvc_users_and_logins.DataAccess;
using learning_aspnetcore_mvc_users_and_logins.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace learning_aspnetcore_mvc_users_and_logins.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly AppDbContext _dbContext;

        public CustomerController(ILogger<CustomerController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        
        public IActionResult Orders()
        {
            var orders = _dbContext.Orders
                .Include(o => o.User)
                .ThenInclude(u => u.Role)
                .ToList()
                .Select(
                    o => new OrderVM
                    {
                        ProductName = o.Product,
                        Quantity = o.Quantity,
                        Price = o.Price,
                        Total = o.Quantity * o.Price,
                        CustomerName = o.User.Login,
                        RoleName = o.User.Role.ToString()
                    });

            return View(orders);
        }
    }
}
