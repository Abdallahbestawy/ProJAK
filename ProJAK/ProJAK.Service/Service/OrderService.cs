using ProJAK.Domain.Entities;
using ProJAK.Domain.Enum;
using ProJAK.Repository.IRepository;
using ProJAK.Repository.Repository;
using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.OrderDto;
using ProJAK.Service.IService;

namespace ProJAK.Service.Service
{
    public class OrderService : IOrderService
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;
        #endregion

        #region ctor
        public OrderService(UnitOfWork unitOfWork, ICartService cartService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _cartService = cartService;
        }
        #endregion

        #region AddOrder
        public async Task<Response<object>> AddOrderAsync(string userId, OrderDto addOrderDto)
        {
            try
            {
                var newOrder = new Order
                {
                    OrderAmount = addOrderDto.TotalAmount,
                    OrderDate = DateTime.UtcNow,
                    StatusOrder = StatusOrder.Pending,
                    UserId = userId
                };
                var resultOrder = await _unitOfWork.Orders.AddAsync(newOrder);
                if (resultOrder == null)
                {
                    return Response<object>.BadRequest("Failed to add Order.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save Order.");
                }
                var newProductOrder = addOrderDto.orderDetails.Select(productOrder => new OrderedProduct
                {
                    OrderId = newOrder.Id,
                    Price = productOrder.Price,
                    ProductId = productOrder.ProductId,
                    Quantity = productOrder.Quantity,
                }).ToList();
                var resultProductOrder = await _unitOfWork.OrderedProducts.AddRangeAsync(newProductOrder);
                if (resultProductOrder == null)
                {
                    return Response<object>.BadRequest("Failed to add Order.");
                }

                var saveProductOrder = await _unitOfWork.SaveAsync();
                if (!saveProductOrder)
                {
                    return Response<object>.BadRequest("Failed to save Order.");
                }
                await _cartService.DeletProductCartsForUserAsync(userId);
                return Response<object>.Created("Order added successfully.");
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while adding the Order.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetOrderAdmin
        public async Task<Response<List<GetOrderDto>>> GetOrderAdminAsync()
        {
            try
            {
                var orderEntity = await _unitOfWork.Orders.FindWithIncludeIEnumerableAsync(u => u.User);
                if (!orderEntity.Any())
                {
                    return Response<List<GetOrderDto>>.NoContent();
                }
                var orderDtos = orderEntity.Select(orderDto => new GetOrderDto
                {
                    Id = orderDto.Id,
                    OrderAmount = orderDto.OrderAmount,
                    OrderDate = orderDto.OrderDate,
                    StatusOrder = orderDto.StatusOrder,
                    UserName = orderDto.User.Name,
                    UserEmail = orderDto.User.Email,
                }).ToList();
                return Response<List<GetOrderDto>>.Success(orderDtos, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<GetOrderDto>>.ServerError("An error occurred while get the Order.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetOrderByUserId

        public async Task<Response<List<GetOrderDto>>> GetOrderByUserIdAsync(string userId)
        {
            try
            {
                var orderEntity = await _unitOfWork.Orders.GetEntityByPropertyWithIncludeAsync
                    (
                    u => u.UserId == userId,
                    u => u.User
                    );
                if (!orderEntity.Any())
                {
                    return Response<List<GetOrderDto>>.NoContent();
                }
                var orderDtos = orderEntity.Select(orderDto => new GetOrderDto
                {
                    Id = orderDto.Id,
                    OrderAmount = orderDto.OrderAmount,
                    OrderDate = orderDto.OrderDate,
                    StatusOrder = orderDto.StatusOrder,
                    UserName = orderDto.User.Name,
                    UserEmail = orderDto.User.Email,
                }).ToList();
                return Response<List<GetOrderDto>>.Success(orderDtos, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<GetOrderDto>>.ServerError("An error occurred while get the Order.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetOrderByOrderId
        public async Task<Response<OrderDto>> GetOrderByOrderIdAsync(Guid orderId)
        {
            try
            {
                var orderedProductEntity = await _unitOfWork.OrderedProducts.GetEntityByPropertyWithIncludeAsync
                    (
                        o => o.OrderId == orderId,
                        o => o.Order
                    );
                if (!orderedProductEntity.Any())
                {
                    return Response<OrderDto>.NoContent();
                }
                var orderedProductDtos = new OrderDto
                {
                    TotalAmount = orderedProductEntity.FirstOrDefault().Order.OrderAmount,
                    orderDetails = orderedProductEntity.Select(orderedProductDto => new OrderDetailsDto
                    {
                        Id = orderedProductDto.Id,
                        Price = orderedProductDto.Price,
                        ProductId = orderedProductDto.ProductId,
                        Quantity = orderedProductDto.Quantity
                    }).ToList()
                };
                return Response<OrderDto>.Success(orderedProductDtos, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<OrderDto>.ServerError("An error occurred while get the Order.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region DeleteOrder
        public async Task<Response<object>> DeleteOrderAsync(Guid orderId)
        {
            try
            {
                var oldOrder = await _unitOfWork.Orders.GetByIdAsync(orderId);
                if (oldOrder == null)
                {
                    return Response<object>.BadRequest("Order not found with the given ID.");
                }
                var oldOrdereProdect = await _unitOfWork.OrderedProducts.GetEntityByPropertyAsync(o => o.OrderId == orderId);
                if (oldOrdereProdect.Any())
                {
                    await _unitOfWork.OrderedProducts.DeleteRangeAsync(oldOrdereProdect);
                }
                await _unitOfWork.Orders.DeleteAsync(oldOrder);
                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to delete Order.");
                }

                return Response<object>.Created("Order deleteed successfully.");

            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while delete the Order.", new List<string> { ex.Message });
            }
        }
        #endregion


    }
}
