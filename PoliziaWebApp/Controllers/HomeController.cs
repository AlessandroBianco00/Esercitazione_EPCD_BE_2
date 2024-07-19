using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoliziaWebApp.Interfaces;
using PoliziaWebApp.Models;
using PoliziaWebApp.Services;
using System.Diagnostics;

namespace PoliziaWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContext _dbContext;
        private readonly IVerbaleReportService _verbaleReportService;

        public HomeController(ILogger<HomeController> logger, DbContext dbContext, IVerbaleReportService verbaleReportService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _verbaleReportService = verbaleReportService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VisualizzaQuery()
        {
            return View();
        }

        [Authorize(Policy = Policies.IsAdmin)]
        public IActionResult Privacy()
        {
            return View();
        }

        // ACTION CREAZIONE MODEL DB

        public IActionResult AddAnagrafica()
        {
            return View(new Anagrafica());
        }

        [HttpPost]
        public IActionResult AddAnagrafica(Anagrafica anagrafica)
        {
            _dbContext.Anagrafica.Create(anagrafica);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddViolazione()
        {
            return View(new Violazione());
        }

        [HttpPost]
        public IActionResult AddViolazione(Violazione violazione)
        {
            _dbContext.Violazione.Create(violazione);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddVerbale()
        {
            var violazioni = _dbContext.Violazione.GetAll();
            ViewBag.Violazione = violazioni;
            var anagrafiche = _dbContext.Anagrafica.GetAll();
            ViewBag.Anagrafica = anagrafiche;
            return View(new Verbale());
        }

        [HttpPost]
        public IActionResult AddVerbale(Verbale verbale)
        {
            _dbContext.Verbale.Create(verbale);
            return RedirectToAction("Index", "Home");
        }

        // ACTION VISUALIZZAZIONE QUERY

        public IActionResult VerbaliPerTrasgressore()
        {

            var list = _verbaleReportService.GetTotaleVerbali();
            return View(list);
        }

        public IActionResult PuntiPerTrasgressore()
        {

            var list = _verbaleReportService.GetTotalePunti();
            return View(list);
        }

        public IActionResult VerbaliDieciPunti()
        {
            var list = _verbaleReportService.GetDieciPunti();
            return View(list);
        }

        public IActionResult MulteQuattrocento()
        {
            var list = _verbaleReportService.GetMulte400();
            return View(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
