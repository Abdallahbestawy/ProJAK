using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.CategorieDto;

namespace ProJAK.Service.IService
{
    public interface ICategorieService
    {
        Task<Response<object>> AddCategorieAsync(CategorieDto addCategorieDto);
        Task<Response<CategorieDto>> GetCategorieByIdAsync(Guid categorieId);
        Task<Response<List<CategorieDto>>> GetAllCategorieAsync();
        Task<Response<object>> UpdateCategorieAsync(CategorieDto updateCategorieDto);
        Task<Response<object>> DeleteCategorieAsync(Guid categorieId);

    }
}
