using ProJAK.Domain.Entities;
using ProJAK.Repository.IRepository;
using ProJAK.Repository.Repository;
using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.HardDto;
using ProJAK.Service.IService;

namespace ProJAK.Service.Service
{
    public class HardService : IHardService
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor
        public HardService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region AddHard
        public async Task<Response<object>> AddHardAsync(HardDto addHardDto)
        {
            try
            {
                Hard newHard = new Hard
                {
                    Name = addHardDto.Name,
                };

                var result = await _unitOfWork.Hards.AddAsync(newHard);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to add Hard.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save Hard.");
                }

                return Response<object>.Created("Hard added successfully.");
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while adding the Hard.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetAllHard
        public async Task<Response<List<HardDto>>> GetAllHardAsync()
        {
            try
            {
                var hardEntities = await _unitOfWork.Hards.GetAllAsync();
                if (!hardEntities.Any())
                {
                    return Response<List<HardDto>>.NoContent();
                }
                var hardDtos = hardEntities.Select(hardEntitie => new HardDto
                {
                    Id = hardEntitie.Id,
                    Name = hardEntitie.Name,
                }).ToList();
                return Response<List<HardDto>>.Success(hardDtos, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<HardDto>>.ServerError("An error occurred while get the hard.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetHardById
        public async Task<Response<HardDto>> GetHardByIdAsync(Guid hardId)
        {
            try
            {
                var hardsEntity = await _unitOfWork.Hards.GetByIdAsync(hardId);

                if (hardsEntity == null)
                    return Response<HardDto>.NoContent();

                HardDto hardDto = new HardDto
                {
                    Id = hardsEntity.Id,
                    Name = hardsEntity.Name
                };
                return Response<HardDto>.Success(hardDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<HardDto>.ServerError("An error occurred while get the hard.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region UpdateHard
        public async Task<Response<object>> UpdateHardAsync(HardDto updateHardDto)
        {
            try
            {
                var oldHard = await _unitOfWork.Hards.GetByIdAsync(updateHardDto.Id);
                if (oldHard == null)
                {
                    return Response<object>.BadRequest("Hard not found.");
                }
                oldHard.Name = updateHardDto.Name;
                var result = await _unitOfWork.Hards.UpdateAsync(oldHard);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to update hard.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save hard.");
                }

                return Response<object>.Created("Hard updateed successfully.");


            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while update the hard.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region DeleteHard
        public async Task<Response<object>> DeleteHardAsync(Guid hardId)
        {
            try
            {
                var oldHard = await _unitOfWork.Hards.GetByIdAsync(hardId);
                if (oldHard == null)
                {
                    return Response<object>.BadRequest("Hard not found with the given ID.");
                }
                await _unitOfWork.Hards.DeleteAsync(oldHard);
                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to delete hard.");
                }

                return Response<object>.Created("Hard deleteed successfully.");

            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while delete the hard.", new List<string> { ex.Message });
            }
        }
        #endregion
    }
}
