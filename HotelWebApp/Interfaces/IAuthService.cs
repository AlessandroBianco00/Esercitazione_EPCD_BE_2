using HotelWebApp.Models;

namespace HotelWebApp.Interfaces
{
    public interface IAuthService
    {
        User Login(string username, string password);
    }
}
