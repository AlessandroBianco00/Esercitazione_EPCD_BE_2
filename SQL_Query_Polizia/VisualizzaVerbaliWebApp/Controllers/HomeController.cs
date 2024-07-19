using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VisualizzaVerbaliWebApp.Models;
using VisualizzaVerbaliWebApp.Services;

namespace VisualizzaVerbaliWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVerbaleService _verbaleService;

        public HomeController(ILogger<HomeController> logger, IVerbaleService verbaleService)
        {
            _logger = logger;
            _verbaleService = verbaleService;
        }

        public IActionResult Index()
        {
            return View(_verbaleService.GetAll(0).OrderByDescending(a => a.IdVerbale));
        }

        public IActionResult Detail(int id)
        {
            return View(_verbaleService.GetVerbale(id));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
