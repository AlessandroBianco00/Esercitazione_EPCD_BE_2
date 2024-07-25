using HotelWebApp.Dto;
using HotelWebApp.Services.Dao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomApiController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public RoomApiController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult<IEnumerable<CustomerDto>> AllRooms()
        {
            var rooms = _dbContext.Room.GetAllRooms();
            return Ok(rooms);
        }
    }
}
