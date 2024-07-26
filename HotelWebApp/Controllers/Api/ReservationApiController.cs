using HotelWebApp.Dto;
using HotelWebApp.Services.Dao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationApiController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public ReservationApiController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> AllReservations()
        {
            var reservations = _dbContext.Reservation.GetAllReservations();
            return Ok(reservations);
        }

        [HttpGet("{cf}")]
        public ActionResult<IEnumerable<CustomerDto>> AllReservations()
        {
            var reservations = _dbContext.Reservation.GetAllReservations();
            return Ok(reservations);
        }
    }
}
