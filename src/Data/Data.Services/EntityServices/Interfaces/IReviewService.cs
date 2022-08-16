namespace Data.Services.EntityServices.Interfaces
{
    using Data.Services.DtoModels;

    public interface IReviewService
    {
        public ReviewDto GetReview(string reviewId);

        public void AddReview(ReviewDto reviewDto, string productId);

        public void UpdateReview(ReviewDto updatedReviewDto);

        public void DeleteReview(string reviewId);
    }
}
