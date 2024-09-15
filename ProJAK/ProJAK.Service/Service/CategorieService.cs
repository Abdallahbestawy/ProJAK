using ProJAK.Domain.Entities;
using ProJAK.Repository.IRepository;
using ProJAK.Repository.Repository;
using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.CategorieDto;
using ProJAK.Service.IService;

namespace ProJAK.Service.Service
{
    public class CategorieService : ICategorieService
    {

        private readonly IUnitOfWork _unitOfWork;
        public CategorieService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<Response<object>> AddCategorieAsync(CategorieDto addCategorieDto)
        {
            try
            {
                Categorie newCategorie = new Categorie
                {
                    Name = addCategorieDto.Name,
                    CategorieType = addCategorieDto.CategorieType
                };

                var result = await _unitOfWork.Categories.AddAsync(newCategorie);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to add category.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save category.");
                }

                return Response<object>.Created("Category added successfully.");
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while adding the category.", new List<string> { ex.Message });
            }
        }



        public async Task<Response<List<CategorieDto>>> GetAllCategorieAsync()
        {
            try
            {
                var categorieEntities = await _unitOfWork.Categories.GetAllAsync();
                if (!categorieEntities.Any())
                {
                    return Response<List<CategorieDto>>.NoContent();
                }
                var categorieDtos = categorieEntities.Select(categorieEntitie => new CategorieDto
                {
                    Id = categorieEntitie.Id,
                    Name = categorieEntitie.Name,
                    CategorieType = categorieEntitie.CategorieType
                }).ToList();
                return Response<List<CategorieDto>>.Success(categorieDtos, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<CategorieDto>>.ServerError("An error occurred while get the category.", new List<string> { ex.Message });
            }
        }

        public async Task<Response<CategorieDto>> GetCategorieByIdAsync(Guid categorieId)
        {
            try
            {
                var categorieEntity = await _unitOfWork.Categories.GetByIdAsync(categorieId);

                if (categorieEntity == null)
                    return Response<CategorieDto>.NoContent();

                CategorieDto categorieDto = new CategorieDto
                {
                    Id = categorieEntity.Id,
                    Name = categorieEntity.Name,
                    CategorieType = categorieEntity.CategorieType
                };
                return Response<CategorieDto>.Success(categorieDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<CategorieDto>.ServerError("An error occurred while get the category.", new List<string> { ex.Message });
            }

        }
        public async Task<Response<object>> UpdateCategorieAsync(CategorieDto updateCategorieDto)
        {
            try
            {
                var oldCategorie = await _unitOfWork.Categories.GetByIdAsync(updateCategorieDto.Id);
                if (oldCategorie == null)
                {
                    return Response<object>.BadRequest("Category not found.");
                }
                oldCategorie.Name = updateCategorieDto.Name;
                oldCategorie.CategorieType = updateCategorieDto.CategorieType;
                var result = await _unitOfWork.Categories.UpdateAsync(oldCategorie);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to update category.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save category.");
                }

                return Response<object>.Created("Category updateed successfully.");


            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while update the category.", new List<string> { ex.Message });
            }
        }
        public async Task<Response<object>> DeleteCategorieAsync(Guid categorieId)
        {
            try
            {
                var oldCategorie = await _unitOfWork.Categories.GetByIdAsync(categorieId);
                if (oldCategorie == null)
                {
                    return Response<object>.BadRequest("Category not found with the given ID.");
                }
                await _unitOfWork.Categories.DeleteAsync(oldCategorie);
                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to delete category.");
                }

                return Response<object>.Created("Category deleteed successfully.");

            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while delete the category.", new List<string> { ex.Message });
            }
        }


    }
}
