using ProJAK.Domain.Entities;
using ProJAK.Repository.IRepository;
using ProJAK.Repository.Repository;
using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.CartDto;
using ProJAK.Service.IService;

namespace ProJAK.Service.Service
{
    public class CartService : ICartService
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor
        public CartService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region AddCart
        public async Task<Response<object>> AddCartAsync(string userId, List<CartDto> addCartDto)
        {
            try
            {
                var cart = await _unitOfWork.Carts.GetEntityByPropertyAsync(c => c.UserId == userId);
                if (!cart.Any())
                {
                    return Response<object>.BadRequest("Cart not Found.");
                }
                var cartId = cart.First().Id;
                var newproductCart = addCartDto.Select(CartDto => new ProductCart
                {
                    CartId = cartId,
                    ProductId = CartDto.ProductId,
                    Quantity = CartDto.Quantity
                }).ToList();
                var result = await _unitOfWork.ProductCarts.AddRangeAsync(newproductCart);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to add Cart.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save Cart.");
                }

                return Response<object>.Created("Cart added successfully.");
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while adding the Cart.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetCartByUserId
        public async Task<Response<GetCart>> GetCartByUserIdAsync(string userId)
        {
            try
            {
                var cartEntity = await _unitOfWork.ProductCarts.GetEntityByPropertyWithIncludeAsync(
                    p => p.Cart.UserId == userId,
                    p => p.Cart,
                    p => p.Product
                    );
                if (!cartEntity.Any())
                {
                    return Response<GetCart>.NoContent();
                }
                GetCart getCartDto = new GetCart
                {
                    GetCartDetails = cartEntity.Select(getCart => new GetCartDetails
                    {
                        Id = getCart.Id,
                        ProductId = getCart.ProductId,
                        Price = getCart.Quantity * getCart.Product.Price,
                        Quantity = getCart.Quantity
                    }).ToList()
                };
                getCartDto.TotalPrice = getCartDto.GetCartDetails.Sum(cart => cart.Price);

                return Response<GetCart>.Success(getCartDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<GetCart>.ServerError("An error occurred while get the Cart.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region UpdateCart
        public async Task<Response<object>> UpdateCartAsync(List<CartDto> updateCartDto)
        {
            var updateCartEntity = new List<ProductCart>();
            foreach (var updateCart in updateCartDto)
            {
                var oldcart = await _unitOfWork.ProductCarts.GetByIdAsync(updateCart.Id);
                oldcart.ProductId = updateCart.ProductId;
                oldcart.Quantity = updateCart.Quantity;
                updateCartEntity.Add(oldcart);
            }
            await _unitOfWork.ProductCarts.UpdateRangeAsync(updateCartEntity);
            var save = await _unitOfWork.SaveAsync();
            if (!save)
            {
                return Response<object>.BadRequest("Failed to save Cart.");
            }
            return Response<object>.Created("Cart updateed successfully.");

        }
        #endregion

        #region DeleteCart
        public async Task<Response<object>> DeleteCartAsync(Guid CartId)
        {
            try
            {
                var oldCart = await _unitOfWork.ProductCarts.GetByIdAsync(CartId);
                if (oldCart == null)
                {
                    return Response<object>.BadRequest("Cart not found with the given ID.");
                }
                await _unitOfWork.ProductCarts.DeleteAsync(oldCart);
                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to delete Cart.");
                }

                return Response<object>.Created("Cart deleteed successfully.");
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while delete the cart.", new List<string> { ex.Message });
            }
        }
        #endregion
    }
}
