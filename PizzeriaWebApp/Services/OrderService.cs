using PizzeriaWebApp.Context;
using PizzeriaWebApp.Controllers;
using PizzeriaWebApp.Interfaces;
using PizzeriaWebApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PizzeriaWebApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _ctx;

        public OrderService(DataContext dbContext)
        {
            _ctx = dbContext;
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _ctx.Orders.SingleOrDefaultAsync(o => o.OrderId == id);
            return order;
        }

        public async Task<IEnumerable<Order>> GetMyOrders(string username)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Name == username);
            var orders = await _ctx.Orders.Include(o => o.Products).ThenInclude(i => i.Product).Where(p => p.UserId == user.UserId).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetConcludedOrders()
        {
            var orders = await _ctx.Orders.Where(o => o.Status == Status.Concluded).Include(o => o.Products).ThenInclude(p => p.Product).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetProcessedOrders()
        {
            var orders = await _ctx.Orders.Where(o => o.Status == Status.Processed).Include(o => o.Products).ThenInclude(p => p.Product).ToListAsync();
            return orders;
        }

        public async Task<Order> ProcessOrder(int id)
        {
            var initialOrder = await GetById(id);

            if (initialOrder != null)
            {
                initialOrder.Status = (Status)3;
                await _ctx.SaveChangesAsync();
            }

            return initialOrder;
        }

        public async Task<int> CountProcessedOrders()
        {
            var count = await _ctx.Orders.Where(o => o.Status == Status.Processed).CountAsync();
            return count;
        }

        public async Task<decimal> DailyRevenue()
        {
            var count = await _ctx.OrderItems.Include(o => o.Order).Include(o => o.Product).Where(o => o.Order.Date.Date == DateTime.Now.Date /*&& o.Order.Status == Status.Processed*/).SumAsync(o => o.Quantity * o.Product.Price);
            return count;
        }
    }
}
