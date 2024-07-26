using HotelWebApp.Dto;
using HotelWebApp.Interfaces;
using HotelWebApp.Models;
using HotelWebApp.Services.Dao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers
{
    [Authorize(Policy = Policies.IsAdmin)]
    public class ReservationController : Controller
    {
        private readonly DbContext _dbContext;
        private readonly IReservationQueryService _reservationQueryService;

        public ReservationController(DbContext dbContext, IReservationQueryService reservationQueryService)
        {
            _dbContext = dbContext;
            _reservationQueryService = reservationQueryService;
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

        public IActionResult CheckOut(int id)
        {
            var reservation = _reservationQueryService.GetReservationCheckOut(id);
            var services = _reservationQueryService.GetServicesCheckOut(id);
            var checkout = new CheckOutModel { ReservationId = id, RoomNumber_FK = reservation.RoomNumber_FK, CheckInDate = reservation.CheckInDate, CheckOutDate = reservation.CheckOutDate, DailyCost = reservation.DailyCost, Deposit = reservation.Deposit  };
            foreach(var s in services)
            {
                checkout.ServiceAdditionList.Add(s);
            }
            return View(checkout);
        }
    }
}
