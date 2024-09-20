using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.CartDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        #region fields
        private readonly ICartService _CartService;
        #endregion

        #region ctor
        public CartController(ICartService CartService)
        {
            _CartService = CartService;
        }
        #endregion

        #region AddCart
        [HttpPost]
        public async Task<IActionResult> AddCategorie(List<CartDto> addCartDto)
        {
            string userid = "5df8c5d5-35bd-4ebb-8a41-758083e246d1";
            var response = await _CartService.AddCartAsync(userid, addCartDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetAllCart
        [HttpGet("u")]
        public async Task<IActionResult> GetCartByUserId()
        {
            string userid = "5df8c5d5-35bd-4ebb-8a41-758083e246d1";
            var response = await _CartService.GetCartByUserIdAsync(userid);
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
