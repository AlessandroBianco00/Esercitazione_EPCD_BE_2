using Microsoft.AspNetCore.Mvc;
using PizzeriaWebApp.Context;
using PizzeriaWebApp.Interfaces;

namespace PizzeriaWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IProductService _productService;
        private readonly IIngredientService _ingredientService;
        private readonly DataContext _ctx;

        public OrderController(DataContext dbContext, ILogger<OrderController> logger, IProductService productService, IIngredientService ingredientService)
        {
            _logger = logger;
            _ctx = dbContext;
            _productService = productService;
            _ingredientService = ingredientService;
        }

        public async Task<IActionResult> Menu()
        {
            var list = await _productService.GetAll();
            return View(list);
        }

        public async Task<IActionResult> AddToOrder(int id)
        {
            var product = await _productService.GetAll();
            ViewBag.Product = product;
            return View();
        }
    }
}
