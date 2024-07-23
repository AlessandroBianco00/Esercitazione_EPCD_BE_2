using HotelWebApp.Interfaces;
using HotelWebApp.Models;
using System.Data.SqlClient;

namespace HotelWebApp.Services
{
    public class AuthService : IAuthService
    {
        private string connectionString;

        private const string LOGIN_COMMAND = "SELECT UserId, Username, Password FROM Users WHERE Username = @username AND Password = @password";
        private const string ROLES_COMMAND = "SELECT RoleName FROM Roles ro JOIN UserRoles ur ON ro.RoleId = ur.RoleId_FK WHERE ur.UserId_FK = @id";

        public AuthService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("HotelServer")!;
        }

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
    }
}
