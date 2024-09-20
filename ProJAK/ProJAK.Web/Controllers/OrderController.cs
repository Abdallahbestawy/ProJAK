using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.OrderDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region fields
        private readonly IOrderService _OrderService;
        #endregion

        #region ctor
        public OrderController(IOrderService OrderService)
        {
            _OrderService = OrderService;
        }
        #endregion

        #region AddOrder
        [HttpPost]
        public async Task<IActionResult> AddCategorie(OrderDto addOrderDto)
        {
            string userid = "5df8c5d5-35bd-4ebb-8a41-758083e246d1";
            var response = await _OrderService.AddOrderAsync(userid, addOrderDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetOrderAdmin
        [HttpGet("a")]
        public async Task<IActionResult> GetOrderAdmin()
        {
            var response = await _OrderService.GetOrderAdminAsync();
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetOrderByUserId
        [HttpGet("u")]
        public async Task<IActionResult> GetOrderByUserId()
        {
            string userid = "5df8c5d5-35bd-4ebb-8a41-758083e246d1";
            var response = await _OrderService.GetOrderByUserIdAsync(userid);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetOrderByOrderId
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetOrderByOrderId(Guid Id)
        {
            var response = await _OrderService.GetOrderByOrderIdAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region DeleteOrder
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid Id)
        {
            var response = await _OrderService.DeleteOrderAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion
    }
}
