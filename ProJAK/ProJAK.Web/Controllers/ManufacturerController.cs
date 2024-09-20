using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProJAK.Domain.Enum;
using ProJAK.Service.DataTransferObject.ManufacturerDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Authorize(Roles = nameof(UserType.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        #region fields
        private readonly IManufacturerService _manufacturerService;
        #endregion

        #region ctor
        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }
        #endregion

        #region GetManufacturerById

        // Get manufacturer by ID
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetManufacturerById(Guid Id)
        {
            var response = await _manufacturerService.GetManufacturerByIdAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetAllManufacturer
        [HttpGet("a")]
        public async Task<IActionResult> GetAllManufacturer()
        {
            var response = await _manufacturerService.GetAllManufacturerAsync();
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region AddManufacturer
        // Add a new manufacturer
        [HttpPost]
        public async Task<IActionResult> AddManufacturer(ManufacturerDto addManufacturerDto)
        {
            var response = await _manufacturerService.AddManufacturerAsync(addManufacturerDto);
            return StatusCode(response.StatusCode, response);

        }
        #endregion

        #region UpdateManufacturer
        // Update an existing manufacturer
        [HttpPut]
        public async Task<IActionResult> UpdateManufacturer(ManufacturerDto updateManufacturerDto)
        {
            var response = await _manufacturerService.UpdateManufacturerAsync(updateManufacturerDto);
            return StatusCode(response.StatusCode, response);

        }
        #endregion

        #region DeleteManufacturer
        // Delete a manufacturer by ID
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteManufacturer(Guid id)
        {
            var response = await _manufacturerService.DeleteManufacturerAsync(id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion
    }
}
