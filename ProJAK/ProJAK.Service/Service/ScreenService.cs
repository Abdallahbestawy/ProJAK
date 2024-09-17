using ProJAK.Domain.Entities;
using ProJAK.Repository.IRepository;
using ProJAK.Repository.Repository;
using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.ScreenDto;
using ProJAK.Service.IService;

namespace ProJAK.Service.Service
{
    public class ScreenService : IScreenService
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor
        public ScreenService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region AddScreen
        public async Task<Response<object>> AddScreenAsync(ScreenDto addScreenDto)
        {
            try
            {
                Screen newScreen = new Screen
                {
                    PanelType = addScreenDto.PanelType,
                    RefreshRate = addScreenDto.RefreshRate,
                    Resolution = addScreenDto.Resolution,
                };

                var result = await _unitOfWork.Screens.AddAsync(newScreen);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to add Screen.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save Screen.");
                }

                return Response<object>.Created("Processor added successfully.");
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while adding the Screen.", new List<string> { ex.Message });
            }
        }
        #endregion


        #region GetAllScreen
        public async Task<Response<List<ScreenDto>>> GetAllScreenAsync()
        {
            try
            {
                var ScreenEntities = await _unitOfWork.Screens.GetAllAsync();
                if (!ScreenEntities.Any())
                {
                    return Response<List<ScreenDto>>.NoContent();
                }
                var ScreenDtos = ScreenEntities.Select(ScreenEntitie => new ScreenDto
                {
                    Id = ScreenEntitie.Id,
                    Resolution = ScreenEntitie.Resolution,
                    RefreshRate = ScreenEntitie.RefreshRate,
                    PanelType = ScreenEntitie.PanelType
                }).ToList();
                return Response<List<ScreenDto>>.Success(ScreenDtos, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<ScreenDto>>.ServerError("An error occurred while get the Screen.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetScreenById
        public async Task<Response<ScreenDto>> GetScreenByIdAsync(Guid ScreenId)
        {
            try
            {
                var ScreensEntity = await _unitOfWork.Screens.GetByIdAsync(ScreenId);

                if (ScreensEntity == null)
                    return Response<ScreenDto>.NoContent();

                ScreenDto ScreenDto = new ScreenDto
                {
                    Id = ScreensEntity.Id,
                    PanelType = ScreensEntity.PanelType,
                    RefreshRate = ScreensEntity.RefreshRate,
                    Resolution = ScreensEntity.Resolution
                };
                return Response<ScreenDto>.Success(ScreenDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<ScreenDto>.ServerError("An error occurred while get the Screen.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region UpdateScreen
        public async Task<Response<object>> UpdateScreenAsync(ScreenDto updateScreenDto)
        {
            try
            {
                var oldScreen = await _unitOfWork.Screens.GetByIdAsync(updateScreenDto.Id);
                if (oldScreen == null)
                {
                    return Response<object>.BadRequest("Screen not found.");
                }
                oldScreen.RefreshRate = updateScreenDto.RefreshRate;
                oldScreen.Resolution = updateScreenDto.Resolution;
                oldScreen.PanelType = updateScreenDto.PanelType;
                var result = await _unitOfWork.Screens.UpdateAsync(oldScreen);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to update Screen.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save Screen.");
                }

                return Response<object>.Created("Screen updateed successfully.");


            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while update the Screen.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region DeleteScreen
        public async Task<Response<object>> DeleteScreenAsync(Guid ScreenId)
        {
            try
            {
                var oldScreen = await _unitOfWork.Screens.GetByIdAsync(ScreenId);
                if (oldScreen == null)
                {
                    return Response<object>.BadRequest("Screen not found with the given ID.");
                }
                await _unitOfWork.Screens.DeleteAsync(oldScreen);
                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to delete Screen.");
                }

                return Response<object>.Created("Screen deleteed successfully.");

            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while delete the Screen.", new List<string> { ex.Message });
            }
        }
        #endregion
    }
}
