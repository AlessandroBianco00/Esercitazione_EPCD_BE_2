using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult AddCustomer()
        {
            return View();
        }
    }
}
