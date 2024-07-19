using PoliziaWebApp.Models;

namespace PoliziaWebApp.Interfaces
{
    public interface IAuthService
    {
        Utente Login(string username, string password);
    }
}
