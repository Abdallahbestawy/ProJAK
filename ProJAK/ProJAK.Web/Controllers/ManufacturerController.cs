using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.ManufacturerDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        // Get manufacturer by ID
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetManufacturerById(Guid id)
        {
            var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return Ok(manufacturer);
        }

        // Add a new manufacturer
        [HttpPost]
        public async Task<IActionResult> AddManufacturer(ManufacturerDto addManufacturerDto)
        {
            bool result = await _manufacturerService.AddManufacturerAsync(addManufacturerDto);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        // Update an existing manufacturer
        [HttpPut]
        public async Task<IActionResult> UpdateManufacturer(ManufacturerDto updateManufacturerDto)
        {
            bool result = await _manufacturerService.UpdateManufacturerAsync(updateManufacturerDto);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        // Delete a manufacturer by ID
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteManufacturer(Guid id)
        {
            bool result = await _manufacturerService.DeleteManufacturerAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
