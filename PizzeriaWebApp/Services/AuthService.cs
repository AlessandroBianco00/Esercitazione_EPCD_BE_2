using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PizzeriaWebApp.Context;
using PizzeriaWebApp.Interfaces;
using PizzeriaWebApp.Models.Entities;

namespace PizzeriaWebApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly DataContext _ctx;

        public AuthService(IPasswordEncoder passwordEncoder, IConfiguration config, DataContext dataContext) : base()
        {
            _passwordEncoder = passwordEncoder;
            _ctx = dataContext;
        }
        public async Task<User> CreateUser(User user)
        {
            var u = new User
            {
                Password = user.Password,
                Email = user.Email,
                Name = user.Name
            };
            _ctx.Users.Add(u);
            await _ctx.SaveChangesAsync();
            return u;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _ctx.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && _passwordEncoder.IsSame(password, user.Password))
            {
                return user;
            }

            return null;
        }

        public async Task<User> Register(User user)
        {
            var u = await CreateUser(
            new User
            {
                Password = _passwordEncoder.Encode(user.Password),
                Email = user.Email,
                Name = user.Name
            });
            var userRole = _ctx.Roles.FirstOrDefault(r => r.RoleName == "User");
            if (userRole != null)
            {
                user.Roles.Add(userRole);
            }

            return new User { UserId = u.UserId, Email = u.Email, Password = u.Password, Name = u.Name };
        }
    }
}
