using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using PizzeriaWebApp.Models.ViewModels;
using PizzeriaWebApp.Models.Entities;
using PizzeriaWebApp.Context;
using PizzeriaWebApp.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PizzeriaWebApp.Controllers
{
    [Authorize(Policy = Policies.IsAdmin)]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IIngredientService _ingredientService;
        private readonly DataContext _ctx;

        public ProductController(DataContext dbContext, ILogger<ProductController> logger, IProductService productService, IIngredientService ingredientService)
        {
            _logger = logger;
            _ctx = dbContext;
            _productService = productService;
            _ingredientService = ingredientService;
        }

        public async Task<IActionResult> AddProduct()
        {
            ViewBag.Ingredients = await _ingredientService.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductViewModel model, IEnumerable<int> ingredients)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(model, ingredients);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> AllProducts()
        {
            var list = await _productService.GetAll();
            return View(list);
        }
    }
}
