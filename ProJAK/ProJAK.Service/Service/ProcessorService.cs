using ProJAK.Domain.Entities;
using ProJAK.Repository.IRepository;
using ProJAK.Repository.Repository;
using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.ProcessorDto;
using ProJAK.Service.IService;

namespace ProJAK.Service.Service
{
    public class ProcessorService : IProcessorService
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor
        public ProcessorService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region AddProcessor
        public async Task<Response<object>> AddProcessorAsync(ProcessorDto addProcessorDto)
        {
            try
            {
                Processor newProcessor = new Processor
                {
                    Name = addProcessorDto.Name,
                    ManufacturerId = addProcessorDto.ManufacturerId,
                };

                var result = await _unitOfWork.Processors.AddAsync(newProcessor);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to add Processor.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save Processor.");
                }

                return Response<object>.Created("Processor added successfully.");
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while adding the Processor.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetAllProcessor
        public async Task<Response<List<ProcessorDto>>> GetAllProcessorAsync()
        {
            try
            {
                var ProcessorEntities = await _unitOfWork.Processors.GetAllAsync();
                if (!ProcessorEntities.Any())
                {
                    return Response<List<ProcessorDto>>.NoContent();
                }
                var ProcessorDtos = ProcessorEntities.Select(ProcessorEntitie => new ProcessorDto
                {
                    Id = ProcessorEntitie.Id,
                    Name = ProcessorEntitie.Name,
                    ManufacturerId = ProcessorEntitie.ManufacturerId
                }).ToList();
                return Response<List<ProcessorDto>>.Success(ProcessorDtos, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<ProcessorDto>>.ServerError("An error occurred while get the Processor.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetProcessorById
        public async Task<Response<ProcessorDto>> GetProcessorByIdAsync(Guid ProcessorId)
        {
            try
            {
                var ProcessorsEntity = await _unitOfWork.Processors.GetByIdAsync(ProcessorId);

                if (ProcessorsEntity == null)
                    return Response<ProcessorDto>.NoContent();

                ProcessorDto ProcessorDto = new ProcessorDto
                {
                    Id = ProcessorsEntity.Id,
                    Name = ProcessorsEntity.Name,
                    ManufacturerId = ProcessorsEntity.ManufacturerId
                };
                return Response<ProcessorDto>.Success(ProcessorDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<ProcessorDto>.ServerError("An error occurred while get the Processor.", new List<string> { ex.Message });
            }

        }
        #endregion

        #region UpdateProcessor
        public async Task<Response<object>> UpdateProcessorAsync(ProcessorDto updateProcessorDto)
        {
            try
            {
                var oldProcessor = await _unitOfWork.Processors.GetByIdAsync(updateProcessorDto.Id);
                if (oldProcessor == null)
                {
                    return Response<object>.BadRequest("Processor not found.");
                }
                oldProcessor.Name = updateProcessorDto.Name;
                oldProcessor.ManufacturerId = updateProcessorDto.ManufacturerId;
                var result = await _unitOfWork.Processors.UpdateAsync(oldProcessor);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to update Processor.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save Processor.");
                }

                return Response<object>.Created("Processor updateed successfully.");


            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while update the Processor.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region DeleteProcessor
        public async Task<Response<object>> DeleteProcessorAsync(Guid ProcessorId)
        {
            try
            {
                var oldProcessor = await _unitOfWork.Processors.GetByIdAsync(ProcessorId);
                if (oldProcessor == null)
                {
                    return Response<object>.BadRequest("Processor not found with the given ID.");
                }
                await _unitOfWork.Processors.DeleteAsync(oldProcessor);
                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to delete Processor.");
                }

                return Response<object>.Created("Processor deleteed successfully.");

            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while delete the Processor.", new List<string> { ex.Message });
            }
        }
        #endregion
    }
}
