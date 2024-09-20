using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.CartDto;

namespace ProJAK.Service.IService
{
    public interface ICartService
    {
        Task<Response<object>> AddCartAsync(string userId, List<CartDto> addCartDto);
        Task<Response<GetCart>> GetCartByUserIdAsync(string userId);
        Task<Response<object>> UpdateCartAsync(List<CartDto> updateCartDto);
        Task<Response<object>> DeleteCartAsync(Guid CartId);
        Task DeletProductCartsForUserAsync(string userId);

    }
}
