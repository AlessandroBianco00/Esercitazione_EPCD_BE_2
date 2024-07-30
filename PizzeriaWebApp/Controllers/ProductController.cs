using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using PizzeriaWebApp.Models.ViewModels;
using PizzeriaWebApp.Models.Entities;
using PizzeriaWebApp.Context;
using PizzeriaWebApp.Interfaces;

namespace PizzeriaWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly DataContext _ctx;

        public ProductController(DataContext dbContext, ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _ctx = dbContext;
            _productService = productService;
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
