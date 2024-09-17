using ProJAK.Domain.Entities;
using ProJAK.Repository.IRepository;
using ProJAK.Repository.Repository;
using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.ManufacturerDto;
using ProJAK.Service.IService;

namespace ProJAK.Service.Service
{
    public class ManufacturerService : IManufacturerService
    {
        #region feilds
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor
        public ManufacturerService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region AddManufacturer
        public async Task<Response<object>> AddManufacturerAsync(ManufacturerDto addManufacturerDto)
        {
            try
            {
                Manufacturer newManufacturer = new Manufacturer
                {
                    Name = addManufacturerDto.Name,

                };
                var result = await _unitOfWork.Manufacturers.AddAsync(newManufacturer);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to add manufacturer.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save manufacturer.");
                }

                return Response<object>.Created("Manufacturer added successfully.");
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while adding the manufacturer.", new List<string> { ex.Message });

            }
        }
        #endregion

        #region GetAllManufacturer
        public async Task<Response<List<ManufacturerDto>>> GetAllManufacturerAsync()
        {
            try
            {
                var manufacturerEntities = await _unitOfWork.Manufacturers.GetAllAsync();
                if (!manufacturerEntities.Any())
                {
                    return Response<List<ManufacturerDto>>.NoContent();
                }
                var manufacturerDtos = manufacturerEntities.Select(manufacturerEntitie => new ManufacturerDto
                {
                    Id = manufacturerEntitie.Id,
                    Name = manufacturerEntitie.Name
                }).ToList();
                return Response<List<ManufacturerDto>>.Success(manufacturerDtos, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<ManufacturerDto>>.ServerError("An error occurred while get the manufacturer.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetManufacturerById
        public async Task<Response<ManufacturerDto>> GetManufacturerByIdAsync(Guid manufacturerId)
        {
            try
            {
                var ManufacturerEntity = await _unitOfWork.Manufacturers.GetByIdAsync(manufacturerId);

                if (ManufacturerEntity == null)
                    return Response<ManufacturerDto>.NoContent();

                ManufacturerDto manufacturerDto = new ManufacturerDto
                {
                    Id = ManufacturerEntity.Id,
                    Name = ManufacturerEntity.Name,
                };
                return Response<ManufacturerDto>.Success(manufacturerDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<ManufacturerDto>.ServerError("An error occurred while get the manufacturer.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region UpdateManufacturer
        public async Task<Response<object>> UpdateManufacturerAsync(ManufacturerDto updateManufacturerDto)
        {
            try
            {
                Manufacturer existingManufacturer = await _unitOfWork.Manufacturers.GetByIdAsync(updateManufacturerDto.Id);
                if (existingManufacturer == null)
                {
                    return Response<object>.BadRequest("manufacturer not found.");
                }

                existingManufacturer.Name = updateManufacturerDto.Name;

                var result = await _unitOfWork.Manufacturers.UpdateAsync(existingManufacturer);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to update manufacturer.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save manufacturer.");
                }
                return Response<object>.Created("Manufacturer updateed successfully.");


            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while update the manufacturer.", new List<string> { ex.Message });

            }
        }
        #endregion

        #region DeleteManufacturer
        public async Task<Response<object>> DeleteManufacturerAsync(Guid manufacturerId)
        {
            try
            {
                var existingManufacturer = await _unitOfWork.Manufacturers.GetByIdAsync(manufacturerId);

                if (existingManufacturer == null)
                {
                    return Response<object>.BadRequest("Manufacturer not found with the given ID.");
                }

                await _unitOfWork.Manufacturers.DeleteAsync(existingManufacturer);
                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to delete manufacturer.");
                }

                return Response<object>.Created("Manufacturer deleteed successfully.");

            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while delete the manufacturer.", new List<string> { ex.Message });
            }
        }
        #endregion

    }
}
