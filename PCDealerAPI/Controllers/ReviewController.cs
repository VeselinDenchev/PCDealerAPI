namespace PCDealerAPI.Controllers
{
    using Constants;

    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using System.Security.Claims;

    [Route(ControllerConstant.CONTROLLER_BASE_ROUTE)]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public ReviewController(IReviewService reviewService)
        {
            this.ReviewService = reviewService;
        }

        public IReviewService ReviewService { get; set; }

        [HttpGet]
        [Route(ControllerConstant.GET_ALL_PRODUCT_REVIEWS_ROUTE)]
        public IActionResult GetAllProductReviews([FromRoute] string productId)
        {
            try
            {
                ReviewDto[] review = this.ReviewService.GetAllReviewsForProduct(productId);

                return Ok(review);
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }


        }

        [HttpGet]
        [Route(ControllerConstant.REVIEW_ID_PARAMETER)]
        public IActionResult GetReview([FromRoute] string reviewId)
        {
            ReviewDto review = this.ReviewService.GetReview(reviewId);

            if (review is null) return NotFound(ErrorMessage.NON_EXISTING_REVIEW_MESSAGE);

            return Ok(review);
        }

        [HttpPost]
        [Route(ControllerConstant.ADD_PRODUCT_REVIEW)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddReview([FromRoute] string productId, [FromForm] ReviewDto review)
        {
            string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            this.ReviewService.AddReview(review, productId, userName);

            return Ok(review);
        }

        [HttpPut]
        [Route(ControllerConstant.UPDATE_ROUTE + ControllerConstant.REVIEW_ID_PARAMETER)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateReview(string reviewId, [FromForm] ReviewDto review)
        {
            try
            {
                string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                review.Id = reviewId;
                this.ReviewService.UpdateReview(review, userName);

                return Ok(review);

            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
            catch (UnauthorizedAccessException uae)
            {
                return Unauthorized(uae.Message);
            }
        }

        [HttpDelete]
        [Route(ControllerConstant.DELETE_ROUTE + ControllerConstant.REVIEW_ID_PARAMETER)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteReview(string reviewId)
        {
            try
            {
                this.ReviewService.DeleteReview(reviewId);

                return Ok(InfoMessage.REVIEW_SUCCESSFULLY_DELETED_MESSAGE);
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }
    }
}
