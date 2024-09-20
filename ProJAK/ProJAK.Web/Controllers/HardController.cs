using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProJAK.Domain.Enum;
using ProJAK.Service.DataTransferObject.HardDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Authorize(Roles = nameof(UserType.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class HardController : ControllerBase
    {
        #region fields
        private readonly IHardService _hardService;
        #endregion

        #region ctor
        public HardController(IHardService hardService)
        {
            _hardService = hardService;
        }
        #endregion

        #region AddHard
        [HttpPost]
        public async Task<IActionResult> AddCategorie(HardDto addHardDto)
        {
            var response = await _hardService.AddHardAsync(addHardDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetHardById
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetHardById(Guid Id)
        {
            var response = await _hardService.GetHardByIdAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion

        #region GetAllHard
        [HttpGet("a")]
        public async Task<IActionResult> GetAllHard()
        {
            var response = await _hardService.GetAllHardAsync();
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region UpdateHard
        [HttpPut]
        public async Task<IActionResult> UpdateHard(HardDto updateHardDto)
        {
            var response = await _hardService.UpdateHardAsync(updateHardDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region DeleteHard
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteHard(Guid Id)
        {
            var response = await _hardService.DeleteHardAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion
    }
}
