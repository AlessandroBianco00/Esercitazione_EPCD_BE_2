using HotelWebApp.Dto;
using HotelWebApp.Interfaces;
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
        private readonly IReservationQueryService _reservationQueryService;

        public ReservationApiController(DbContext dbContext, IReservationQueryService reservationQueryService)
        {
            _dbContext = dbContext;
            _reservationQueryService = reservationQueryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> AllReservations()
        {
            var reservations = _dbContext.Reservation.GetAllReservations();
            return Ok(reservations);
        }

        [HttpGet("{cf}")]
        public ActionResult<IEnumerable<ReservationDto>> ReservationByFC([FromRoute] string cf)
        {
            var reservations = _reservationQueryService.GetReservationByFC(cf);
            return Ok(reservations);
        }

        [HttpGet("halfboard")]
        public ActionResult<IEnumerable<ReservationDto>> HalfBoardReservation()
        {
            var reservations = _reservationQueryService.GetReservationFullBoard();
            return Ok(reservations);
        }
    }
}
