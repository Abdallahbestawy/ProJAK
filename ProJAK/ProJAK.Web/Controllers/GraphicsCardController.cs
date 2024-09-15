using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.GraphicsCardDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphicsCardController : ControllerBase
    {
        private readonly IGraphicsCardService _graphicsCardService;

        public GraphicsCardController(IGraphicsCardService graphicsCardService)
        {
            _graphicsCardService = graphicsCardService;
        }
        [HttpPost]
        public async Task<IActionResult> AddGraphicsCard(GraphicsCardDto addGraphicsCardDto)
        {
            var response = await _graphicsCardService.AddGraphicsCardAsync(addGraphicsCardDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetGraphicsCardById(Guid Id)
        {
            var response = await _graphicsCardService.GetGraphicsCardByIdAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("a")]
        public async Task<IActionResult> GeAlltGraphicsCard()
        {
            var response = await _graphicsCardService.GetAllGraphicsCardAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGraphicsCard(GraphicsCardDto updateGraphicsCardDto)
        {
            var response = await _graphicsCardService.UpdateGraphicsCardAsync(updateGraphicsCardDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteGraphicsCard(Guid Id)
        {
            var response = await _graphicsCardService.DeleteGraphicsCardAsync(Id);
            return StatusCode(response.StatusCode, response);
        }

    }
}
