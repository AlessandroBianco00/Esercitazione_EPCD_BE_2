using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult AddService()
        {
            return View();
        }
    }
}
