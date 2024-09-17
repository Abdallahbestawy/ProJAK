using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.ReviewDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        #region fields
        private readonly IReviewService _reviewService;
        #endregion

        #region ctor
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        #endregion

        #region AddReview
        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewDto addReviewDto)
        {
            string uId = "220b7236-5290-4630-8d4d-71e9780d06a6";
            var response = await _reviewService.AddReviewAsync(uId, addReviewDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetReviewByProductId
        [HttpGet("rp/{Id:guid}")]
        public async Task<IActionResult> GetReviewByProductId(Guid Id)
        {
            var response = await _reviewService.GetReviewByProductIdAsync(Id);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetReviewByUserId
        [HttpGet("up/{Id:guid}")]
        public async Task<IActionResult> GetReviewByUserId(Guid Id)
        {
            string userId = "";
            var response = await _reviewService.GetReviewByUserIdAsync(userId, Id);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region UpdateReview
        [HttpPut]
        public async Task<IActionResult> UpdateReview(ReviewDto updateReviewDto)
        {
            string userId = "220b7236-5290-4630-8d4d-71e9780d06a6";
            var response = await _reviewService.UpdateReviewAsync(userId, updateReviewDto);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region DeleteReview
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteReview(Guid Id)
        {
            string userId = "220b7236-5290-4630-8d4d-71e9780d06a6";
            var response = await _reviewService.DeleteReviewAsync(userId, Id);
            return StatusCode(response.StatusCode, response);
        }
        #endregion
    }
}
