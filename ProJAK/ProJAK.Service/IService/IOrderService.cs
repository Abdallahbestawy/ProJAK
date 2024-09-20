using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.OrderDto;

namespace ProJAK.Service.IService
{
    public interface IOrderService
    {
        Task<Response<object>> AddOrderAsync(string userId, OrderDto addOrderDto);
        Task<Response<List<GetOrderDto>>> GetOrderAdminAsync();
        Task<Response<List<GetOrderDto>>> GetOrderByUserIdAsync(string userId);
        Task<Response<OrderDto>> GetOrderByOrderIdAsync(Guid orderId);
        Task<Response<object>> DeleteOrderAsync(Guid orderId);
    }
}
