using PoliziaWebApp.Interfaces;
using PoliziaWebApp.Models;
using System.Data.SqlClient;

namespace PoliziaWebApp.Services
{
    public class AuthService : IAuthService
    {
        private string connectionString;

        private const string LOGIN_COMMAND = "SELECT UtenteId, Username, Password FROM Utenti WHERE Username = @username AND Password = @password";
        private const string ROLES_COMMAND = "SELECT NomeRuolo FROM Ruoli ro JOIN RuoliUtenti ur ON ro.RuoloId = ur.RuoloId_FK WHERE UtenteId_FK = @id";

        public AuthService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("PoliziaMunicipale")!;
        }

        public Utente Login(string username, string password)
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
                    var u = new Utente { Id = r.GetInt32(0), Password = password, Username = username };
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
