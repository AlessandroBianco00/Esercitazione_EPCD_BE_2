using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzeriaWebApp.Context;
using PizzeriaWebApp.Interfaces;
using PizzeriaWebApp.Models.Entities;
using PizzeriaWebApp.Services;

namespace PizzeriaWebApp.Controllers
{
    [Authorize(Policy = Policies.IsAdmin)]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IProductService _productService;
        private readonly IIngredientService _ingredientService;
        private readonly IOrderService _orderService;
        private readonly DataContext _ctx;

        public AdminController(DataContext dbContext, ILogger<AdminController> logger, IProductService productService, IIngredientService ingredientService, IOrderService orderService)
        {
            _logger = logger;
            _ctx = dbContext;
            _productService = productService;
            _ingredientService = ingredientService;
            _orderService = orderService;
        }
        public async Task<IActionResult> ConcludedOrders()
        {
            var orders = await _orderService.GetConcludedOrders();
            return View(orders);
        }

        public async Task<IActionResult> DailyRecap()
        {
            return View();
        }
    }
}
