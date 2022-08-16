namespace Data.Services.EntityServices.Interfaces
{
    using Data.Services.DtoModels;

    public interface IReviewService
    {
        public ReviewDto[] GetAllReviewsForProduct(string productId);

        public ReviewDto GetReview(string reviewId);

        public void AddReview(ReviewDto reviewDto, string productId, string userId);

        public void UpdateReview(ReviewDto updatedReviewDto);

        public void DeleteReview(string reviewId);
    }
}
