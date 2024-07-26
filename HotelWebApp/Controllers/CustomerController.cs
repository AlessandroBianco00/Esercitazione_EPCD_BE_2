using HotelWebApp.Dto;
using HotelWebApp.Services.Dao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers
{
    [Authorize(Policy = Policies.IsAdmin)]
    public class CustomerController : Controller
    {
        private readonly DbContext _dbContext;

        public CustomerController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCustomer(CustomerDto customer)
        {
            _dbContext.Customer.CreateCustomer(customer);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AllCustomers()
        {
            var customers = _dbContext.Customer.GetAllCustomers();
            return View(customers);
        }

        public IActionResult DeleteCustomer(string fiscalCode)
        {
            _dbContext.Customer.DeleteCustomer(fiscalCode);
            return Json(new {success = true});
        }

        public IActionResult AllCustomersJson()
        {
            _dbContext.Customer.GetAllCustomers();
            return Json(new { success = true });
        }
    }
}

