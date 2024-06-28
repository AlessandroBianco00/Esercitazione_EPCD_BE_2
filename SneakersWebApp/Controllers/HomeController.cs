using Microsoft.AspNetCore.Mvc;
using SneakersWebApp.Models;
using SneakersWebApp.Services;
using System.Diagnostics;

namespace SneakersWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISneakerService _sneakerService;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, ISneakerService sneakerService, IWebHostEnvironment env)
        {
            _logger = logger;
            _sneakerService = sneakerService;
            _env = env;
        }

        public IActionResult Index()
        {
            //var testSneaker = new Sneaker { ProductName = "Air Jordan I", ProductDescription = "La scarpa da cui è nato tutto, la Air Jordan 1 è probabilmente il più amato modello di sempre, così come //quello che più ha rivoluzionato la storia delle sneakers.", Price = 150, ProductId = 0, ProductCover = 0.jpeg };
            var sneakers = _sneakerService.GetAllProducts();
            return View(sneakers);
        }

        public IActionResult AggiungiProdotto()
        {
            ViewData["ListaProdotti"] = _sneakerService.GetAllProducts();
            return View(new Sneaker());
        }

        [HttpPost]
        public IActionResult AggiungiProdotto(Sneaker sneaker)
        {
            //var prodotto = new Product { ProductName = inputProdotto.ProductName, Description = inputProdotto.Description, QuantityAvailable = inputProdotto.QuantityAvailable };
            _sneakerService.CreateSneaker(sneaker);
            string uploads = Path.Combine(_env.WebRootPath, "images");
            if (sneaker.ProductCover.Length > 0)
            {
                string filePath = Path.ChangeExtension(Path.Combine(uploads, sneaker.ProductId.ToString()), "jpg");
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                sneaker.ProductCover.CopyTo(fileStream);
            }
            if (sneaker.ProductImage1.Length > 0)
            {
                string filePath = Path.ChangeExtension(Path.Combine(uploads, $"{sneaker.ProductId.ToString()}_extra1"), "jpg");
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                sneaker.ProductImage1.CopyTo(fileStream);
            }
            if (sneaker.ProductImage2.Length > 0)
            {
                string filePath = Path.ChangeExtension(Path.Combine(uploads, $"{sneaker.ProductId.ToString()}_extra2"), "jpg");
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                sneaker.ProductImage2.CopyTo(fileStream);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int id)
        {
            var sneaker = _sneakerService.GetById(id);
            string uploads = Path.Combine(_env.WebRootPath, "images");
            var cover = Path.ChangeExtension(Path.Combine(uploads, sneaker.ProductId.ToString()), "jpg");
            var extra1 = Path.ChangeExtension(Path.Combine(uploads, $"{sneaker.ProductId.ToString()}_extra1"), "jpg");
            var extra2 = Path.ChangeExtension(Path.Combine(uploads, $"{sneaker.ProductId.ToString()}_extra2"), "jpg");
            if (System.IO.File.Exists(cover))
                ViewBag.Cover = $"/images/{sneaker.ProductId}.jpg";
            if (System.IO.File.Exists(extra1))
                ViewBag.Extra1 = $"/images/{sneaker.ProductId}_extra1.jpg";
            if (System.IO.File.Exists(extra2))
                ViewBag.Extra2 = $"/images/{sneaker.ProductId}_extra2.jpg";
            return View(sneaker);
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
