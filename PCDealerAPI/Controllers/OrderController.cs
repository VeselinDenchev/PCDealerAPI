namespace PCDealerAPI.Controllers
{
    using System.Security.Claims;

    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        public OrderController(IOrderService orderService)
        {
            this.OrderService = orderService;
        }

        public IOrderService OrderService { get; set; }

        [HttpGet]
        [EnableCors("MyCorsPolicy")]
        [Route("get/all")]
        public IActionResult GetUserOrders()
        {
            string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            OrderDto[] orders = this.OrderService.GetAccountOrders(userName);

            return Ok(orders);
        }

        [HttpGet]
        [EnableCors("MyCorsPolicy")]
        [Route("get/{orderId}")]
        public IActionResult GetOrder([FromRoute] string orderId)
        {
            OrderDto order = this.OrderService.GetOrder(orderId);

            if (order is null) return NotFound("Such order doesn't exist!");

            return Ok(order);
        }

        [HttpPost]
        [EnableCors("MyCorsPolicy")]
        [Route("add")]
        public IActionResult AddOrder([FromForm] OrderDto order)
        {
            string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                this.OrderService.AddOrder(order, userName);
            }
            catch (ArgumentException ae)
            {
                if (ae.GetType() == typeof(ArgumentOutOfRangeException)) return BadRequest(ae.Message);

                return NotFound(ae.Message);
            }


            return Ok(order);
        }

        [HttpDelete]
        [EnableCors("MyCorsPolicy")]
        [Route("delete/{orderId}")]
        public IActionResult DeleteOrder(string orderId)
        {
            try
            {
                this.OrderService.DeleteOrder(orderId);

                return Ok("The order is successfully deleted");
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }
    }
}
