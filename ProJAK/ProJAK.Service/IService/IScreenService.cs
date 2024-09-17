using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.ScreenDto;

namespace ProJAK.Service.IService
{
    public interface IScreenService
    {
        Task<Response<object>> AddScreenAsync(ScreenDto addScreenDto);
        Task<Response<ScreenDto>> GetScreenByIdAsync(Guid ScreenId);
        Task<Response<List<ScreenDto>>> GetAllScreenAsync();
        Task<Response<object>> UpdateScreenAsync(ScreenDto updateScreenDto);
        Task<Response<object>> DeleteScreenAsync(Guid ScreenId);
    }
}
