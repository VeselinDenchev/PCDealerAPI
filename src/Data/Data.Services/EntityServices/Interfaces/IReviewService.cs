namespace Data.Services.EntityServices.Interfaces
{
    using Data.Models.Entities;
    using Data.Services.DtoModels;

    public interface IReviewService
    {
        public ReviewDto[] GetAllReviewsForProduct(string productId);

        public ReviewDto GetReview(string reviewId);

        public void AddReview(ReviewDto reviewDto, string productId, string userName);

        public void UpdateReview(ReviewDto updatedReviewDto, string userName);

        public void DeleteReview(string reviewId);
    }
}
