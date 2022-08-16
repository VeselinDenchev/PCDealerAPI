namespace Data.Services.EntityServices
{
    using AutoMapper;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ReviewService : IReviewService
    {
        public ReviewService(PcDealerDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public PcDealerDbContext DbContext { get; init; }

        public IMapper Mapper { get; set; }

        public ReviewDto[] GetAllReviewsForProduct(string productId)
        {
            bool exists = this.DbContext.Products.Any(p => p.Id == productId && p.IsDeleted == false);
            if (!exists) throw new ArgumentException("Such product doesn't exist!");

            Review[] reviews = this.DbContext.Reviews.Where(r => r.Product.Id == productId && r.IsDeleted == false)
                                                        .Include(r => r.User)
                                                        .ToArray();

            ReviewDto[] reviewDtos = this.Mapper.Map<Review[], ReviewDto[]>(reviews);

            return reviewDtos;
        }

        public ReviewDto GetReview(string reviewId)
        {
            Review review = this.DbContext.Reviews.Where(r => r.Id == reviewId && r.IsDeleted == false).FirstOrDefault();
            ReviewDto reviewDto = this.Mapper.Map<Review, ReviewDto>(review);

            return reviewDto;
        }

        public void AddReview(ReviewDto reviewDto, string productId, string userId)
        {
            bool exists = this.DbContext.Reviews.Any(r => r.Id == productId && r.IsDeleted == false);
            if (!exists) throw new ArgumentException("Such product doesn't exist!");

            User user = this.DbContext.Users.Where(u => u.Id == userId).First();

            reviewDto.User = user;

            Review review = this.Mapper.Map<ReviewDto, Review>(reviewDto);

            Product product = this.DbContext.Products.Where(p => p.Id == productId && p.IsDeleted == false).FirstOrDefault();

            review.Product = product;

            this.DbContext.Reviews.Add(review);
            this.DbContext.SaveChanges();

            reviewDto.CreatedAtUtc = DateTime.UtcNow;
            reviewDto.ModifiedAtUtc = DateTime.UtcNow;
        }

        public void UpdateReview(ReviewDto updatedReviewDto)
        {
            bool exists = this.DbContext.Reviews.Any(b => b.Id == updatedReviewDto.Id);
            if (!exists) throw new ArgumentException("Such review doesn't exist!");

            Review review = this.Mapper.Map<ReviewDto, Review>(updatedReviewDto);

            this.DbContext.Reviews.Update(review);
            this.DbContext.SaveChanges();

            updatedReviewDto.ModifiedAtUtc = DateTime.UtcNow;
        }

        public void DeleteReview(string reviewId)
        {
            bool exists = this.DbContext.Reviews.Any(b => b.Id == reviewId);
            if (!exists) throw new ArgumentException("Such review doesn't exist!");

            Review review = this.DbContext.Reviews.Where(r => r.Id == reviewId && r.IsDeleted == false).First();

            review.IsDeleted = true;
            review.DeletedAtUtc = DateTime.UtcNow;

            this.DbContext.Reviews.Update(review);
            this.DbContext.SaveChanges();
        }
    }
}
