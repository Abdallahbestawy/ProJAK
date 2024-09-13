using ProJAK.Domain.Entities;
using ProJAK.Repository.IRepository;
using ProJAK.Repository.Repository;
using ProJAK.Service.DataTransferObject.ManufacturerDto;
using ProJAK.Service.IService;

namespace ProJAK.Service.Service
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ManufacturerService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<bool> AddManufacturerAsync(ManufacturerDto addManufacturerDto)
        {
            try
            {
                Manufacturer newManufacturer = new Manufacturer
                {
                    Name = addManufacturerDto.Name,

                };
                await _unitOfWork.Manufacturers.AddAsync(newManufacturer);
                var result = await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public async Task<ManufacturerDto> GetManufacturerByIdAsync(Guid manufacturerId)
        {
            try
            {
                var ManufacturerEntity = await _unitOfWork.Manufacturers.GetByIdAsync(manufacturerId);

                if (ManufacturerEntity == null)
                    return null;

                ManufacturerDto manufacturerDto = new ManufacturerDto
                {
                    Id = ManufacturerEntity.Id,
                    Name = ManufacturerEntity.Name,
                };
                return manufacturerDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdateManufacturerAsync(ManufacturerDto updateManufacturerDto)
        {
            try
            {
                Manufacturer existingManufacturer = await _unitOfWork.Manufacturers.GetByIdAsync(updateManufacturerDto.Id);
                if (existingManufacturer == null)
                    return false;

                existingManufacturer.Name = updateManufacturerDto.Name;

                await _unitOfWork.Manufacturers.UpdateAsync(existingManufacturer);
                var result = await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteManufacturerAsync(Guid manufacturerId)
        {
            try
            {
                var existingManufacturer = await _unitOfWork.Manufacturers.GetByIdAsync(manufacturerId);

                if (existingManufacturer == null)
                    return false;

                await _unitOfWork.Manufacturers.DeleteAsync(existingManufacturer);
                var result = await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
