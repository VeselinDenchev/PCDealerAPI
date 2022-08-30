namespace PCDealerAPI.Controllers
{
    using System.Security.Claims;

    using Constants;

    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route(ControllerConstant.CONTROLLER_BASE_ROUTE)]
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
        [Route(ControllerConstant.ALL_ROUTE)]
        public IActionResult GetUserOrders()
        {
            string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            OrderDto[] orders = this.OrderService.GetAccountOrders(userName);

            return Ok(orders);
        }

        [HttpGet]
        [Route(ControllerConstant.ORDER_ID_PARAMETER)]
        public IActionResult GetOrder([FromRoute] string orderId)
        {
            OrderDto order = this.OrderService.GetOrder(orderId);

            if (order is null) return NotFound(ErrorMessage.NON_EXISTING_ORDER_MESSAGE);

            return Ok(order);
        }

        [HttpPost]
        [Route(ControllerConstant.ADD_ROUTE)]
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
        [Route(ControllerConstant.DELETE_ROUTE + ControllerConstant.ORDER_ID_PARAMETER)]
        public IActionResult DeleteOrder(string orderId)
        {
            try
            {
                this.OrderService.DeleteOrder(orderId);

                return Ok(InfoMessage.ORDER_SUCCESSFULLY_DELETED_MESSAGE);
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }
    }
}