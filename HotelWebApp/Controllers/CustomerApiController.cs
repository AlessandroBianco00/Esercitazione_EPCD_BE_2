using HotelWebApp.Dto;
using HotelWebApp.Services.Dao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public CustomerApiController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult<IEnumerable<CustomerDto>> AllCustomers()
        {
            var customers = _dbContext.Customer.GetAllCustomers();
            return Ok(customers);
        }
    }
}
