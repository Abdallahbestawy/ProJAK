using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.CategorieDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        #region fields
        private readonly ICategorieService _categorieService;
        #endregion

        #region ctor
        public CategorieController(ICategorieService categorieService)
        {
            _categorieService = categorieService;
        }
        #endregion

        #region AddCategorie
        [HttpPost]
        public async Task<IActionResult> AddCategorie(CategorieDto addCategorieDto)
        {
            var response = await _categorieService.AddCategorieAsync(addCategorieDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetCategorieById
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetCategorieById(Guid Id)
        {
            var response = await _categorieService.GetCategorieByIdAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion

        #region GetAllCategorie
        [HttpGet("a")]
        public async Task<IActionResult> GetAllCategorie()
        {
            var response = await _categorieService.GetAllCategorieAsync();
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region UpdateCategorie
        [HttpPut]
        public async Task<IActionResult> UpdateCategorie(CategorieDto updateCategorieDto)
        {
            var response = await _categorieService.UpdateCategorieAsync(updateCategorieDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region DeleteCategorie
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteCategorie(Guid Id)
        {
            var response = await _categorieService.DeleteCategorieAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        #endregion
    }
}
