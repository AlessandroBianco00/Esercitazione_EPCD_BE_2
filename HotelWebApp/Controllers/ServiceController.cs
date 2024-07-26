using HotelWebApp.Dto;
using HotelWebApp.Models;
using HotelWebApp.Services.Dao;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers
{
    public class ServiceController : Controller
    {
        private readonly DbContext _dbContext;

        public ServiceController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult AddReservationService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReservationService(ServiceAddition serviceAddition)
        {
            var reservationService = new ReservationServiceDto { ReservationId_FK = serviceAddition.ReservationId_FK, ServiceId_FK = serviceAddition.ServiceId, Quantity = serviceAddition.Quantity, Date = serviceAddition.Date};
            _dbContext.ReservationService.AddReservationService(reservationService);
            return RedirectToAction("Index", "Home");
        }
    }
}
