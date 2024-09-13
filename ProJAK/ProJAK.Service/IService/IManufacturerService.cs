using ProJAK.Service.DataTransferObject.ManufacturerDto;

namespace ProJAK.Service.IService
{
    public interface IManufacturerService
    {
        Task<bool> AddManufacturerAsync(ManufacturerDto addManufacturerDto);
        Task<bool> UpdateManufacturerAsync(ManufacturerDto updateManufacturerDto);
        Task<bool> DeleteManufacturerAsync(Guid manufacturerId);
        Task<ManufacturerDto> GetManufacturerByIdAsync(Guid manufacturerId);
    }
}
