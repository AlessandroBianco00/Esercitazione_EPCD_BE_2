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
        private readonly IOrderService _orderService;
        private readonly DataContext _ctx;

        public OrderController(DataContext dbContext, ILogger<OrderController> logger, IProductService productService, IIngredientService ingredientService, IOrderService orderService)
        {
            _logger = logger;
            _ctx = dbContext;
            _productService = productService;
            _ingredientService = ingredientService;
            _orderService = orderService;
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

        public async Task<IActionResult> ProcessOrder(int id)
        {
            var proccessedOrder = await _orderService.ProcessOrder(id);
            return RedirectToAction("ConcludedOrders", "Admin");
        }

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
            if (user == null)
            {
                return NotFound("User not found");
            }

            var validProductIds = await _ctx.Products.Select(p => p.ProductId).ToListAsync();

            //Log prodotti per debug
            Console.WriteLine("Products in the order form:");
            foreach (var product in model.Products)
            {
                Console.WriteLine($"ProductId: {product.ProductId}, ProductName: {product.ProductName}, Quantity: {product.Quantity}, Valid: {validProductIds.Contains(product.ProductId)}");
            }


            var orderItems = model.Products
                                  .Where(p => p.Quantity > 0 && validProductIds.Contains(p.ProductId))
                                  .Select(p => new OrderItem
                                  {
                                      ProductId = p.ProductId,
                                      Quantity = p.Quantity
                                  }).ToList();

            if (!orderItems.Any())
            {
                return BadRequest("No valid products in the order");
            }

            var order = new Order
            {
                Address = model.Address,
                Notes = model.Notes,
                Status = Status.Concluded,
                UserId = user.UserId,
                Products = orderItems
            };

            _ctx.Orders.Add(order);
            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log the error (uncomment ex variable name and write a log.)
                Console.WriteLine($"An error occurred while saving the order: {ex.Message}");
                // Re-throw or handle as needed
                throw;
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> YourOrders()
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Name == User.Identity.Name);
            var orders = await _orderService.GetMyOrders(user.Name);
            return View(orders);
        }
    }
}
