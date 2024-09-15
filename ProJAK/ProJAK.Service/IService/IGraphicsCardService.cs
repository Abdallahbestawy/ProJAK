using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.GraphicsCardDto;

namespace ProJAK.Service.IService
{
    public interface IGraphicsCardService
    {
        Task<Response<object>> AddGraphicsCardAsync(GraphicsCardDto addGraphicsCardDto);
        Task<Response<GraphicsCardDto>> GetGraphicsCardByIdAsync(Guid graphicsCardId);
        Task<Response<List<GraphicsCardDto>>> GetAllGraphicsCardAsync();
        Task<Response<object>> UpdateGraphicsCardAsync(GraphicsCardDto updateGraphicsCardDto);
        Task<Response<object>> DeleteGraphicsCardAsync(Guid graphicsCardId);
    }
}
