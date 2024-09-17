using ProJAK.Domain.Entities;
using ProJAK.Repository.IRepository;
using ProJAK.Repository.Repository;
using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.ReviewDto;
using ProJAK.Service.IService;

namespace ProJAK.Service.Service
{
    public class ReviewService : IReviewService
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor
        public ReviewService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        #endregion

        #region AddReview
        public async Task<Response<object>> AddReviewAsync(string userId, ReviewDto addReviewDto)
        {
            try
            {
                Review newReview = new Review
                {
                    ProductId = addReviewDto.ProductId,
                    UserId = userId,
                    ReviewText = addReviewDto.ReviewText,
                    ReviewDate = DateTime.UtcNow,
                };

                var result = await _unitOfWork.Reviews.AddAsync(newReview);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to add Review.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save Review.");
                }

                return Response<object>.Created("GraphicsCard added Review.");
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while adding the Review.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetReviewByProductId
        public async Task<Response<List<GetReviewDto>>> GetReviewByProductIdAsync(Guid productId)
        {
            try
            {
                var reviews = await _unitOfWork.Reviews.GetEntityByPropertyAsync(p => p.ProductId == productId);
                if (!reviews.Any())
                {
                    return Response<List<GetReviewDto>>.NoContent();
                }
                var reviewsDto = reviews.Select(review => new GetReviewDto
                {
                    ReviewDate = review.ReviewDate,
                    ReviewText = review.ReviewText
                }).ToList();
                return Response<List<GetReviewDto>>.Success(reviewsDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<GetReviewDto>>.ServerError("An error occurred while get the Review.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetReviewByUserId
        public async Task<Response<List<GetReviewDto>>> GetReviewByUserIdAsync(string userId, Guid productId)
        {
            try
            {
                var reviews = await _unitOfWork.Reviews.GetEntityByPropertyAsync(p => p.ProductId == productId && p.UserId == userId);
                if (!reviews.Any())
                {
                    return Response<List<GetReviewDto>>.NoContent();
                }
                var reviewsDto = reviews.Select(review => new GetReviewDto
                {
                    ReviewDate = review.ReviewDate,
                    ReviewText = review.ReviewText
                }).ToList();
                return Response<List<GetReviewDto>>.Success(reviewsDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<GetReviewDto>>.ServerError("An error occurred while get the Review.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region UpdateReview
        public async Task<Response<object>> UpdateReviewAsync(string userId, ReviewDto updateReviewDto)
        {
            try
            {
                var oldReview = await _unitOfWork.Reviews.GetEntityByPropertyAsync(p => p.UserId == userId && p.Id == updateReviewDto.Id);
                if (!oldReview.Any())
                {
                    return Response<object>.BadRequest("Review not found.");
                }
                oldReview.FirstOrDefault().ReviewText = updateReviewDto.ReviewText;
                var result = await _unitOfWork.Reviews.UpdateAsync(oldReview.FirstOrDefault());
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to update review.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save graphicsCard.");
                }

                return Response<object>.Updated("Review updateed successfully.");
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while update the Review.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region DeleteReview
        public async Task<Response<object>> DeleteReviewAsync(string userId, Guid reviewId)
        {
            try
            {
                var oldReview = await _unitOfWork.Reviews.GetEntityByPropertyAsync(p => p.Id == reviewId && p.UserId == userId);
                if (!oldReview.Any())
                {
                    return Response<object>.BadRequest("Review not found with the given ID.");
                }
                await _unitOfWork.Reviews.DeleteAsync(oldReview.FirstOrDefault());
                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to delete review.");
                }

                return Response<object>.Created("reviews deleteed successfully.");

            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while delete the review.", new List<string> { ex.Message });
            }
        }
        #endregion
    }
}
