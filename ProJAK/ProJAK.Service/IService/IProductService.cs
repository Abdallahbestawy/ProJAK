using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.ProductDto;

namespace ProJAK.Service.IService
{
    public interface IProductService
    {
        Task<Response<object>> AddProductAsync(ProductDto addProductDto);
        Task<Response<ProductDto>> GetProductByIdAsync(Guid ProductId);
        Task<Response<List<ProductDto>>> GetAllProductAsync();
        Task<Response<List<ProductDto>>> GetAllProductByCategorieAsync(Guid categorieId);
        Task<Response<object>> UpdateProductAsync(ProductDto updateProductDto);
        Task<Response<object>> DeleteProductAsync(Guid ProductId);


    }
}
