using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.ProductDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region fields
        private readonly IProductService _ProductService;
        #endregion

        #region ctor
        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }
        #endregion

        #region AddProduct
        [HttpPost]
        public async Task<IActionResult> AddCategorie(ProductDto addProductDto)
        {
            var response = await _ProductService.AddProductAsync(addProductDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetProductById

        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetProductById(Guid Id)
        {
            var response = await _ProductService.GetProductByIdAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion

        #region GetAllProduct
        [HttpGet("a")]
        public async Task<IActionResult> GetAllProduct()
        {
            var response = await _ProductService.GetAllProductAsync();
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetProductByCategorieId

        [HttpGet("aCategorie/{categorieId:guid}")]
        public async Task<IActionResult> GetProductByCategorieId(Guid categorieId)
        {
            var response = await _ProductService.GetAllProductByCategorieAsync(categorieId);
            return StatusCode(response.StatusCode, response);

        }
        #endregion

        #region UpdateProduct
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto updateProductDto)
        {
            var response = await _ProductService.UpdateProductAsync(updateProductDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region DeleteProduct
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
            var response = await _ProductService.DeleteProductAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion
    }
}
