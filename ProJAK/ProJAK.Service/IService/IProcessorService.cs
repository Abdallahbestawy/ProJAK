using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.ProcessorDto;

namespace ProJAK.Service.IService
{
    public interface IProcessorService
    {
        Task<Response<object>> AddProcessorAsync(ProcessorDto addProcessorDto);
        Task<Response<ProcessorDto>> GetProcessorByIdAsync(Guid ProcessorId);
        Task<Response<List<ProcessorDto>>> GetAllProcessorAsync();
        Task<Response<object>> UpdateProcessorAsync(ProcessorDto updateProcessorDto);
        Task<Response<object>> DeleteProcessorAsync(Guid ProcessorId);
    }
}
