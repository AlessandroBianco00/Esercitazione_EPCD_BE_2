using HotelWebApp.Dto;
using HotelWebApp.Services.Dao;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers
{
    public class ReservationController : Controller
    {
        private readonly DbContext _dbContext;

        public ReservationController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult AllReservations()
        {
            return View();
        }

        public IActionResult AddReservation()
        {
            var roooms = _dbContext.Room.GetAllRooms();
            ViewBag.Rooms = roooms;
            var customers = _dbContext.Customer.GetAllCustomers();
            ViewBag.Customers = customers;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReservation(ReservationDto reservation)
        {
            _dbContext.Reservation.AddReservation(reservation);
            return RedirectToAction(nameof(AllReservations));
        }

        public IActionResult AllRooms()
        {
            var rooms = _dbContext.Room.GetAllRooms();
            return View(rooms);
        }
    }
}
