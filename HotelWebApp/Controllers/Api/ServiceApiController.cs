using HotelWebApp.Dto;
using HotelWebApp.Services.Dao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceApiController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public ServiceApiController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ServiceDto>> AllServices()
        {
            var services = _dbContext.Service.GetAllServices();
            return Ok(services);
        }

        [HttpGet("{id}")] // GET /api/serviceapi/10
        public IActionResult ServiceById([FromRoute] int id)
        {
            var service = _dbContext.Service.GetServiceById(id);
            return Ok(service);
        }
    }
}
