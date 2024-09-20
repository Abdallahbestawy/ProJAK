using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.CartDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        #region fields
        private readonly ICartService _CartService;
        private readonly IHelpureService _HelpureService;
        #endregion

        #region ctor
        public CartController(ICartService CartService, IHelpureService helpureService)
        {
            _CartService = CartService;
            _HelpureService = helpureService;
        }
        #endregion

        #region AddCart
        [HttpPost]
        public async Task<IActionResult> AddCategorie(List<CartDto> addCartDto)
        {
            var currentUserId = await _HelpureService.GetUserAsync(User);
            if (currentUserId == null)
            {
                return Unauthorized();
            }
            var response = await _CartService.AddCartAsync(currentUserId, addCartDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetAllCart
        [HttpGet("u")]
        public async Task<IActionResult> GetCartByUserId()
        {
            var currentUserId = await _HelpureService.GetUserAsync(User);
            if (currentUserId == null)
            {
                return Unauthorized();
            }
            var response = await _CartService.GetCartByUserIdAsync(currentUserId);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region UpdateCart
        [HttpPut]
        public async Task<IActionResult> UpdateCart(List<CartDto> updateCartDto)
        {
            var response = await _CartService.UpdateCartAsync(updateCartDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region DeleteCart
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteCart(Guid Id)
        {
            var response = await _CartService.DeleteCartAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion
    }
}
