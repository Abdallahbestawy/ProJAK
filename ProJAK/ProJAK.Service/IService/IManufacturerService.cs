using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.ManufacturerDto;

namespace ProJAK.Service.IService
{
    public interface IManufacturerService
    {
        Task<Response<object>> AddManufacturerAsync(ManufacturerDto addManufacturerDto);
        Task<Response<ManufacturerDto>> GetManufacturerByIdAsync(Guid manufacturerId);
        Task<Response<List<ManufacturerDto>>> GetAllManufacturerAsync();
        Task<Response<object>> UpdateManufacturerAsync(ManufacturerDto updateManufacturerDto);
        Task<Response<object>> DeleteManufacturerAsync(Guid manufacturerId);


    }
}
