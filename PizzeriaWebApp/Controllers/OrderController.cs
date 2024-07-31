using Microsoft.AspNetCore.Mvc;
using PizzeriaWebApp.Context;
using PizzeriaWebApp.Interfaces;
using PizzeriaWebApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using PizzeriaWebApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace PizzeriaWebApp.Controllers
{
    [Authorize]
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

        /*public async Task<IActionResult> MakeAnOrder()
        {
            var list = await _productService.GetAll();
            ViewBag.Products = list;
            return View();
        }*/

        public async Task<IActionResult> MakeAnOrder()
        {
            var products = await _ctx.Products.Select(p => new ProductOrderModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price
            }).ToListAsync();

            var model = new OrderFormModel { Products = products };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeAnOrder(OrderFormModel model, string Username)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Name == Username);
            var order = new Order
            {
                Address = model.Address,
                Notes = model.Notes,
                Status = Status.Incomplete,
                UserId = user.UserId,
                Products = model.Products.Where(p => p.Quantity > 0).Select(p => new OrderItem
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList()
            };

            _ctx.Orders.Add(order);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> YourOrders()
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Name == "ccc");
            return View();
        }
    }
}
