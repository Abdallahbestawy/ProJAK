using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.ReviewDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        #region fields
        private readonly IReviewService _reviewService;
        private readonly IHelpureService _helpureService;
        #endregion

        #region ctor
        public ReviewController(IReviewService reviewService, IHelpureService helpureService)
        {
            _reviewService = reviewService;
            _helpureService = helpureService;
        }
        #endregion

        #region AddReview
        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewDto addReviewDto)
        {
            var currentUserId = await _helpureService.GetUserAsync(User);
            if (currentUserId == null)
            {
                return Unauthorized();
            }
            var response = await _reviewService.AddReviewAsync(currentUserId, addReviewDto);

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
            var currentUserId = await _helpureService.GetUserAsync(User);
            if (currentUserId == null)
            {
                return Unauthorized();
            }
            var response = await _reviewService.GetReviewByUserIdAsync(currentUserId, Id);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region UpdateReview
        [HttpPut]
        public async Task<IActionResult> UpdateReview(ReviewDto updateReviewDto)
        {
            var currentUserId = await _helpureService.GetUserAsync(User);
            if (currentUserId == null)
            {
                return Unauthorized();
            }
            var response = await _reviewService.UpdateReviewAsync(currentUserId, updateReviewDto);
            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region DeleteReview
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> DeleteReview(Guid Id)
        {
            var currentUserId = await _helpureService.GetUserAsync(User);
            if (currentUserId == null)
            {
                return Unauthorized();
            }
            var response = await _reviewService.DeleteReviewAsync(currentUserId, Id);
            return StatusCode(response.StatusCode, response);
        }
        #endregion
    }
}
