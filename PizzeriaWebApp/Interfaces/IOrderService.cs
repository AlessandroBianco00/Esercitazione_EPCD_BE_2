using PizzeriaWebApp.Models.Entities;

namespace PizzeriaWebApp.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> GetById(int id);
        public Task<Order> ProcessOrder(int id);
        public Task<IEnumerable<Order>> GetMyOrders(string username);
        public Task<IEnumerable<Order>> GetConcludedOrders();
    }
}
