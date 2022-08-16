namespace PCDealerAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using System.Security.Claims;

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public ReviewController(IReviewService reviewService)
        {
            this.ReviewService = reviewService;
        }

        public IReviewService ReviewService { get; set; }

        [HttpGet]
        [EnableCors("MyCorsPolicy")]
        [Route("product/{productId}/get/all")]
        public IActionResult GetAllReviewsForProduct([FromRoute] string productId)
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
        [EnableCors("MyCorsPolicy")]
        [Route("get/{reviewId}")]
        public IActionResult GetReview([FromRoute] string reviewId)
        {
            ReviewDto review = this.ReviewService.GetReview(reviewId);

            if (review is null) return NotFound("Such review doesn't exist!");

            return Ok(review);
        }

        [HttpPost]
        [EnableCors("MyCorsPolicy")]
        [Route("product/{productId}/add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddReview([FromRoute] string productId, [FromForm] ReviewDto review)
        {
            string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            this.ReviewService.AddReview(review, productId, userName);

            return Ok(review);
        }

        [HttpPut]
        [EnableCors("MyCorsPolicy")]
        [Route("update/{reviewId}")]
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
        [EnableCors("MyCorsPolicy")]
        [Route("delete/{reviewId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteReview(string reviewId)
        {
            try
            {
                this.ReviewService.DeleteReview(reviewId);

                return Ok("The review is successfully deleted");
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }
    }
}
