using HotelWebApp.Dto;
using HotelWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Interfaces
{
    public interface IAuthService
    {
        User Login(string username, string password);
        public User CreateUser(User user);
        public User Register(User user);
    }
}
