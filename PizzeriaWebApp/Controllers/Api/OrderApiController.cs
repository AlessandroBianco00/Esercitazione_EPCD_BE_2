using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzeriaWebApp.Context;
using PizzeriaWebApp.Interfaces;

namespace PizzeriaWebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly DataContext _ctx;

        public OrderApiController(DataContext dbContext, IProductService productService, IOrderService orderService)
        {
            _ctx = dbContext;
            _productService = productService;
            _orderService = orderService;
        }

        [HttpGet("processed")]
        public async Task<ActionResult> NumberOfOrders()
        {
            var orderCount = _orderService.CountProcessedOrders();
            return Ok(orderCount);
        }

        [HttpGet("dailyrevenue")]
        public async Task<ActionResult> DailyRevenue()
        {
            var revenue = _orderService.DailyRevenue();
            return Ok(revenue);
        }
    }
}
