using PizzeriaWebApp.Context;
using PizzeriaWebApp.Controllers;
using PizzeriaWebApp.Interfaces;
using PizzeriaWebApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace PizzeriaWebApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _ctx;

        public OrderService(DataContext dbContext)
        {
            _ctx = dbContext;
        }

        public async Task<IEnumerable<Order>> GetMyOrders(string username)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Name == username);
            var orders = await _ctx.Orders.Include(o => o.Products).ThenInclude(i => i.Product).Where(p => p.UserId == user.UserId).ToListAsync();
            return orders;
        }
    }
}
