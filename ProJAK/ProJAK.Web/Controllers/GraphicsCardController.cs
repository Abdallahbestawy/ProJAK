using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.GraphicsCardDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphicsCardController : ControllerBase
    {
        #region fields
        private readonly IGraphicsCardService _graphicsCardService;
        #endregion

        #region ctor
        public GraphicsCardController(IGraphicsCardService graphicsCardService)
        {
            _graphicsCardService = graphicsCardService;
        }
        #endregion

        #region AddGraphicsCard
        [HttpPost]
        public async Task<IActionResult> AddGraphicsCard(GraphicsCardDto addGraphicsCardDto)
        {
            var response = await _graphicsCardService.AddGraphicsCardAsync(addGraphicsCardDto);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetGraphicsCardById
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetGraphicsCardById(Guid Id)
        {
            var response = await _graphicsCardService.GetGraphicsCardByIdAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GeAlltGraphicsCard
        [HttpGet("a")]
        public async Task<IActionResult> GeAlltGraphicsCard()
        {
            var response = await _graphicsCardService.GetAllGraphicsCardAsync();
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region UpdateGraphicsCard
        [HttpPut]
        public async Task<IActionResult> UpdateGraphicsCard(GraphicsCardDto updateGraphicsCardDto)
        {
            var response = await _graphicsCardService.UpdateGraphicsCardAsync(updateGraphicsCardDto);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region DeleteGraphicsCard
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteGraphicsCard(Guid Id)
        {
            var response = await _graphicsCardService.DeleteGraphicsCardAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        #endregion
    }
}
