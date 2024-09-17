using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.ProcessorDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorController : ControllerBase
    {
        #region fields
        private readonly IProcessorService _ProcessorService;
        #endregion

        #region ctor
        public ProcessorController(IProcessorService ProcessorService)
        {
            _ProcessorService = ProcessorService;
        }
        #endregion

        #region AddProcessor
        [HttpPost]
        public async Task<IActionResult> AddCategorie(ProcessorDto addProcessorDto)
        {
            var response = await _ProcessorService.AddProcessorAsync(addProcessorDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetProcessorById
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetProcessorById(Guid Id)
        {
            var response = await _ProcessorService.GetProcessorByIdAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion

        #region GetAllProcessor
        [HttpGet("a")]
        public async Task<IActionResult> GetAllProcessor()
        {
            var response = await _ProcessorService.GetAllProcessorAsync();
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region UpdateProcessor
        [HttpPut]
        public async Task<IActionResult> UpdateProcessor(ProcessorDto updateProcessorDto)
        {
            var response = await _ProcessorService.UpdateProcessorAsync(updateProcessorDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region DeleteProcessor
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteProcessor(Guid Id)
        {
            var response = await _ProcessorService.DeleteProcessorAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion
    }
}
