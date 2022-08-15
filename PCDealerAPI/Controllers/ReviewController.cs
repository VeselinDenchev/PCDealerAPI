namespace PCDealerAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public ReviewController(IReviewService reviewService)
        {
            this.ReviewService = reviewService;
        }

        public IReviewService ReviewService { get; set; }

        // TODO: Get all reviews for product

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
        [Route("add")]
        public IActionResult AddReview([FromForm] ReviewDto review)
        {
            this.ReviewService.AddReview(review);

            return Ok(review);
        }

        [HttpPut]
        [EnableCors("MyCorsPolicy")]
        [Route("update/{reviewId}")]
        public IActionResult UpdateReview(string reviewId, [FromForm] ReviewDto review)
        {
            try
            {
                review.Id = reviewId;
                this.ReviewService.UpdateReview(review);

                return Ok(review);

            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }

        [HttpDelete]
        [EnableCors("MyCorsPolicy")]
        [Route("delete/{reviewId}")]
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
