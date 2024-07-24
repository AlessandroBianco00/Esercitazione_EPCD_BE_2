using HotelWebApp.Interfaces;
using HotelWebApp.Models;
using HotelWebApp.Services.Dao;
using System.Data.SqlClient;
using static HotelWebApp.Interfaces.IPasswordEncoder;
using HotelWebApp.Dto;
using Microsoft.Extensions.Logging;

namespace HotelWebApp.Services
{
    public class AuthService : IAuthService
    {
        private string connectionString;

        private readonly IPasswordEncoder _passwordEncoder;
        public AuthService(IPasswordEncoder passwordEncoder, IConfiguration config) : base()
        {
            _passwordEncoder = passwordEncoder;
            connectionString = config.GetConnectionString("HotelServer")!;
        }

        private const string LOGIN_COMMAND = "SELECT UserId, Username, Password FROM Users WHERE Username = @username AND Password = @password";
        private const string ROLES_COMMAND = "SELECT RoleName FROM Roles ro JOIN UserRoles ur ON ro.RoleId = ur.RoleId_FK WHERE ur.UserId_FK = @id";
        private const string INSERT_USER = "INSERT INTO Users(Username, Password) OUTPUT INSERTED.UserId VALUES(@username, @password)";

        public User Login(string username, string password)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(LOGIN_COMMAND, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    var u = new User { Id = r.GetInt32(0), Password = password, Username = username };
                    r.Close();
                    using var roleCmd = new SqlCommand(ROLES_COMMAND, conn);
                    roleCmd.Parameters.AddWithValue("@id", u.Id);
                    using var re = roleCmd.ExecuteReader();
                    while (re.Read())
                    {
                        u.Roles.Add(re.GetString(0));
                    }
                    return u;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public User CreateUser(User user)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(INSERT_USER, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                user.Id = (int)cmd.ExecuteScalar();
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User Register(User user)
        {
            var u = CreateUser(
            new User
            {
                    Password = _passwordEncoder.Encode(user.Password),
                    Username = user.Username
                });
            return new User { Id = u.Id, Password = u.Password, Username = u.Username};
        }
    }
}
