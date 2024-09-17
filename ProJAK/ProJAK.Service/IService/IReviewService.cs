using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.ReviewDto;

namespace ProJAK.Service.IService
{
    public interface IReviewService
    {
        Task<Response<object>> AddReviewAsync(string userId, ReviewDto addReviewDto);
        Task<Response<List<GetReviewDto>>> GetReviewByProductIdAsync(Guid productId);
        Task<Response<List<GetReviewDto>>> GetReviewByUserIdAsync(string userId, Guid productId);
        Task<Response<object>> UpdateReviewAsync(string userId, ReviewDto updateReviewDto);
        Task<Response<object>> DeleteReviewAsync(string userId, Guid reviewId);
    }
}
