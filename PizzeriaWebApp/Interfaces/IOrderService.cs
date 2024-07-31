using PizzeriaWebApp.Models.Entities;

namespace PizzeriaWebApp.Interfaces
{
    public interface IOrderService
    {
        public Task<IEnumerable<Order>> GetMyOrders(string username);
    }
}
