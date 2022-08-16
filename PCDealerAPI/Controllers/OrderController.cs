namespace PCDealerAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController(IOrderService orderService)
        {
            this.OrderService = orderService;
        }

        public IOrderService OrderService { get; set; }

        // TODO: Get all orders for product

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
            //this.OrderService.AddOrder(order);

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
