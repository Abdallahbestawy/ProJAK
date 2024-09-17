using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.HardDto;

namespace ProJAK.Service.IService
{
    public interface IHardService
    {
        Task<Response<object>> AddHardAsync(HardDto addHardDto);
        Task<Response<HardDto>> GetHardByIdAsync(Guid hardId);
        Task<Response<List<HardDto>>> GetAllHardAsync();
        Task<Response<object>> UpdateHardAsync(HardDto updateHardDto);
        Task<Response<object>> DeleteHardAsync(Guid hardId);
    }
}
