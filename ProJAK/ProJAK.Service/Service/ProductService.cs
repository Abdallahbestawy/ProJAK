using ProJAK.Domain.Entities;
using ProJAK.Repository.IRepository;
using ProJAK.Repository.Repository;
using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.ProductDto;
using ProJAK.Service.IService;

namespace ProJAK.Service.Service
{
    public class ProductService : IProductService
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor
        public ProductService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region AddProduct
        public async Task<Response<object>> AddProductAsync(ProductDto addProductDto)
        {
            try
            {
                var newProduct = new Product
                {
                    CategorieId = addProductDto.CategorieId,
                    Description = addProductDto.Description,
                    Name = addProductDto.Name,
                    GraphicsCardId = addProductDto.GraphicsCardId,
                    HardId = addProductDto.HardId,
                    HardSize = addProductDto.HardSize,
                    ManufacturerId = addProductDto.ManufacturerId,
                    ProcessorId = addProductDto.ProcessorId,
                    Price = addProductDto.Price,
                    ProductColor = addProductDto.ProductColor,
                    ScreenId = addProductDto.ScreenId,
                    Quantity = addProductDto.Quantity,
                    RamSize = addProductDto.RamSize
                };
                var result = await _unitOfWork.Products.AddAsync(newProduct);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to add Product.");
                }
                var saveproduct = await _unitOfWork.SaveAsync();
                if (!saveproduct)
                {
                    return Response<object>.BadRequest("Failed to save Product.");
                }
                var newImages = addProductDto.Image.Select(image => new Image
                {
                    ProductId = newProduct.Id,
                    Path = image.Path,
                    MainImage = image.MainImage
                }).ToList();
                var resultImage = await _unitOfWork.Images.AddRangeAsync(newImages);
                if (resultImage == null)
                {
                    return Response<object>.BadRequest("Failed to add Images.");
                }
                var saveImage = await _unitOfWork.SaveAsync();
                if (!saveImage)
                {
                    return Response<object>.BadRequest("Failed to save Images.");
                }
                return Response<object>.Created("Product added successfully.");

            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while adding the Product.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetProductById
        public async Task<Response<ProductDto>> GetProductByIdAsync(Guid ProductId)
        {
            try
            {
                var productEntity = await _unitOfWork.Products.GetEntityByPropertyWithIncludeAsync(
                    p => p.Id == ProductId,
                    p => p.Images
                );

                if (!productEntity.Any())
                    return Response<ProductDto>.NoContent();
                var productEntityFirst = productEntity.FirstOrDefault();
                ProductDto productDto = new ProductDto
                {
                    Id = productEntityFirst.Id,
                    Name = productEntityFirst.Name,
                    CategorieId = productEntityFirst.CategorieId,
                    Description = productEntityFirst.Description,
                    GraphicsCardId = productEntityFirst.GraphicsCardId,
                    HardId = productEntityFirst.HardId,
                    HardSize = productEntityFirst.HardSize,
                    ManufacturerId = productEntityFirst.ManufacturerId,
                    Price = productEntityFirst.Price,
                    ScreenId = productEntityFirst.ScreenId,
                    ProcessorId = productEntityFirst.ProcessorId,
                    ProductColor = productEntityFirst.ProductColor,
                    Quantity = productEntityFirst.Quantity,
                    RamSize = productEntityFirst.RamSize,
                    Image = productEntityFirst.Images
            .Select(img => new ImageDto
            {
                Id = img.Id,
                Path = img.Path,
                MainImage = img.MainImage
            }).ToList()
                };
                return Response<ProductDto>.Success(productDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<ProductDto>.ServerError("An error occurred while get the Product.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetAllProduct

        public async Task<Response<List<ProductDto>>> GetAllProductAsync()
        {
            try
            {
                var productEntity = await _unitOfWork.Products.FindWithIncludeIEnumerableAsync(p => p.Images);

                if (!productEntity.Any())
                    return Response<List<ProductDto>>.NoContent();
                var productDto = productEntity.Select(product => new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategorieId = product.CategorieId,
                    Description = product.Description,
                    GraphicsCardId = product.GraphicsCardId,
                    HardId = product.HardId,
                    HardSize = product.HardSize,
                    ManufacturerId = product.ManufacturerId,
                    Price = product.Price,
                    ScreenId = product.ScreenId,
                    ProcessorId = product.ProcessorId,
                    ProductColor = product.ProductColor,
                    Quantity = product.Quantity,
                    RamSize = product.RamSize,
                    Image = product.Images
                    .Where(img => img.MainImage == true)
                    .Select(img => new ImageDto
                    {
                        Id = img.Id,
                        Path = img.Path,
                        MainImage = img.MainImage
                    }).ToList()
                }).ToList();
                return Response<List<ProductDto>>.Success(productDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<ProductDto>>.ServerError("An error occurred while get the Product.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetAllProductByCategorieId
        public async Task<Response<List<ProductDto>>> GetAllProductByCategorieAsync(Guid categorieId)
        {
            try
            {
                var productEntity = await _unitOfWork.Products.GetEntityByPropertyWithIncludeAsync(
                   p => p.CategorieId == categorieId
                    , p => p.Images
                    );

                if (!productEntity.Any())
                    return Response<List<ProductDto>>.NoContent();
                var productDto = productEntity.Select(product => new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategorieId = product.CategorieId,
                    Description = product.Description,
                    GraphicsCardId = product.GraphicsCardId,
                    HardId = product.HardId,
                    HardSize = product.HardSize,
                    ManufacturerId = product.ManufacturerId,
                    Price = product.Price,
                    ScreenId = product.ScreenId,
                    ProcessorId = product.ProcessorId,
                    ProductColor = product.ProductColor,
                    Quantity = product.Quantity,
                    RamSize = product.RamSize,
                    Image = product.Images
                    .Where(img => img.MainImage == true)
                    .Select(img => new ImageDto
                    {
                        Id = img.Id,
                        Path = img.Path,
                        MainImage = img.MainImage
                    }).ToList()
                }).ToList();
                return Response<List<ProductDto>>.Success(productDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<ProductDto>>.ServerError("An error occurred while get the Product.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region UpdateProduct
        public async Task<Response<object>> UpdateProductAsync(ProductDto updateProductDto)
        {
            try
            {
                var oldProduct = await _unitOfWork.Products.GetByIdAsync(updateProductDto.Id);
                if (oldProduct == null)
                {
                    return Response<object>.BadRequest("Product not found.");
                }
                oldProduct.ManufacturerId = updateProductDto.ManufacturerId;
                oldProduct.Price = updateProductDto.Price;
                oldProduct.CategorieId = updateProductDto.CategorieId;
                oldProduct.GraphicsCardId = updateProductDto.GraphicsCardId;
                oldProduct.Description = updateProductDto.Description;
                oldProduct.ScreenId = updateProductDto.ScreenId;
                oldProduct.RamSize = updateProductDto.RamSize;
                oldProduct.Quantity = updateProductDto.Quantity;
                oldProduct.HardSize = updateProductDto.HardSize;
                oldProduct.HardId = updateProductDto.HardId;
                oldProduct.ProductColor = updateProductDto.ProductColor;
                oldProduct.Name = updateProductDto.Name;
                oldProduct.ProcessorId = updateProductDto.ProcessorId;
                var oldimage = await _unitOfWork.Images.GetEntityByPropertyAsync(i => i.ProductId == updateProductDto.Id);
                foreach (var imageDto in updateProductDto.Image)
                {
                    var existingImage = oldimage.FirstOrDefault(i => i.Id == imageDto.Id);
                    if (existingImage != null)
                    {
                        existingImage.Path = imageDto.Path;
                        existingImage.MainImage = imageDto.MainImage;
                    }
                    else
                    {
                        var newImage = new Image
                        {
                            Path = imageDto.Path,
                            MainImage = imageDto.MainImage,
                            ProductId = oldProduct.Id
                        };
                        await _unitOfWork.Images.AddAsync(newImage);
                    }
                }
                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save Product.");
                }

                return Response<object>.Created("Product updateed successfully.");

            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while update the Product.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region DeleteProduct
        public async Task<Response<object>> DeleteProductAsync(Guid ProductId)
        {
            try
            {
                var oldProduct = await _unitOfWork.Products.GetByIdAsync(ProductId);
                if (oldProduct == null)
                {
                    return Response<object>.BadRequest("Product not found with the given ID.");
                }
                var imageProduct = await _unitOfWork.Images.GetEntityByPropertyAsync(i => i.ProductId == oldProduct.Id);
                if (imageProduct.Any())
                {
                    await _unitOfWork.Images.DeleteRangeAsync(imageProduct);
                }
                await _unitOfWork.Products.DeleteAsync(oldProduct);
                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to delete Product.");
                }

                return Response<object>.Created("Product deleteed successfully.");

            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while delete the Product.", new List<string> { ex.Message });
            }
        }
        #endregion

    }
}
