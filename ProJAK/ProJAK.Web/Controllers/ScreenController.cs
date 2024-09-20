using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProJAK.Domain.Enum;
using ProJAK.Service.DataTransferObject.ScreenDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Authorize(Roles = nameof(UserType.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenController : ControllerBase
    {
        #region fields
        private readonly IScreenService _ScreenService;
        #endregion

        #region ctor
        public ScreenController(IScreenService ScreenService)
        {
            _ScreenService = ScreenService;
        }
        #endregion

        #region AddScreen
        [HttpPost]
        public async Task<IActionResult> AddCategorie(ScreenDto addScreenDto)
        {
            var response = await _ScreenService.AddScreenAsync(addScreenDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetScreenById
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetScreenById(Guid Id)
        {
            var response = await _ScreenService.GetScreenByIdAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion

        #region GetAllScreen
        [HttpGet("a")]
        public async Task<IActionResult> GetAllScreen()
        {
            var response = await _ScreenService.GetAllScreenAsync();
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region UpdateScreen
        [HttpPut]
        public async Task<IActionResult> UpdateScreen(ScreenDto updateScreenDto)
        {
            var response = await _ScreenService.UpdateScreenAsync(updateScreenDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region DeleteScreen
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteScreen(Guid Id)
        {
            var response = await _ScreenService.DeleteScreenAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion
    }
}
