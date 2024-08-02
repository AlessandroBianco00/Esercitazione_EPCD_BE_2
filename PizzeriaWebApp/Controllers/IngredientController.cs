using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzeriaWebApp.Context;
using PizzeriaWebApp.Interfaces;

namespace PizzeriaWebApp.Controllers
{
    [Authorize(Policy = Policies.IsAdmin)]
    public class IngredientController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IIngredientService _ingredientService;
        private readonly DataContext _ctx;

        public IngredientController(DataContext dbContext, ILogger<ProductController> logger, IIngredientService ingredientService)
        {
            _logger = logger;
            _ctx = dbContext;
            _ingredientService = ingredientService;
        }

        public async Task<IActionResult> AllIngredients()
        {
            var list = await _ingredientService.GetAll();
            return View(list);
        }

        public async Task<IActionResult> DeleteIngredient(int id)
        {
            await _ingredientService.DeleteIngredient(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
