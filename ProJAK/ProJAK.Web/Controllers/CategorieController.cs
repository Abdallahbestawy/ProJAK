using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.CategorieDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private readonly ICategorieService _categorieService;

        public CategorieController(ICategorieService categorieService)
        {
            _categorieService = categorieService;
        }
        [HttpPost]
        public async Task<IActionResult> AddCategorie(CategorieDto addCategorieDto)
        {
            var response = await _categorieService.AddCategorieAsync(addCategorieDto);

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetCategorieById(Guid Id)
        {
            var response = await _categorieService.GetCategorieByIdAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("a")]
        public async Task<IActionResult> GetAllCategorie()
        {
            var response = await _categorieService.GetAllCategorieAsync();
            return StatusCode(response.StatusCode, response);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategorie(CategorieDto updateCategorieDto)
        {
            var response = await _categorieService.UpdateCategorieAsync(updateCategorieDto);

            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteCategorie(Guid Id)
        {
            var response = await _categorieService.DeleteCategorieAsync(Id);
            return StatusCode(response.StatusCode, response);

        }
    }
}
