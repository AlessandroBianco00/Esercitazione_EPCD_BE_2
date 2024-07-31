using PizzeriaWebApp.Models.Entities;

namespace PizzeriaWebApp.Interfaces
{
    public interface IAuthService
    {
        Task<User> Login(string username, string password);
        public Task<User> CreateUser(User user);
        public Task<User> Register(User user);
    }
}
