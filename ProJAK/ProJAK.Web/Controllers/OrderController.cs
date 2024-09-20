using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProJAK.Domain.Enum;
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
        private readonly IHelpureService _helpureService;
        #endregion

        #region ctor
        public OrderController(IOrderService OrderService, IHelpureService helpureService)
        {
            _OrderService = OrderService;
            _helpureService = helpureService;
        }
        #endregion

        #region AddOrder
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCategorie(OrderDto addOrderDto)
        {
            var currentUserId = await _helpureService.GetUserAsync(User);
            if (currentUserId == null)
            {
                return Unauthorized();
            }
            var response = await _OrderService.AddOrderAsync(currentUserId, addOrderDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetOrderAdmin
        [Authorize(Roles = nameof(UserType.Admin))]
        [HttpGet("a")]
        public async Task<IActionResult> GetOrderAdmin()
        {
            var response = await _OrderService.GetOrderAdminAsync();
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetOrderByUserId
        [Authorize]
        [HttpGet("u")]
        public async Task<IActionResult> GetOrderByUserId()
        {
            var currentUserId = await _helpureService.GetUserAsync(User);
            if (currentUserId == null)
            {
                return Unauthorized();
            }
            var response = await _OrderService.GetOrderByUserIdAsync(currentUserId);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetOrderByOrderId
        [Authorize]
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetOrderByOrderId(Guid Id)
        {
            var response = await _OrderService.GetOrderByOrderIdAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region DeleteOrder
        [Authorize]
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid Id)
        {
            var response = await _OrderService.DeleteOrderAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion
    }
}
