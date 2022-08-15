﻿namespace Data.Services.EntityServices
{
    using AutoMapper;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    public class ReviewService : IReviewService
    {
        public ReviewService(PcDealerDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public PcDealerDbContext DbContext { get; init; }

        public IMapper Mapper { get; set; }

        // TODO: Get product reviews

        public ReviewDto GetReview(string reviewId)
        {
            Review review = this.DbContext.Reviews.Where(r => r.Id == reviewId && r.IsDeleted == false).FirstOrDefault();
            ReviewDto reviewDto = this.Mapper.Map<Review, ReviewDto>(review);

            return reviewDto;
        }

        public void AddReview(string productId, ReviewDto reviewDto)
        {
            Review review = this.Mapper.Map<ReviewDto, Review>(reviewDto);

            this.DbContext.Reviews.Add(review);
            this.DbContext.SaveChanges();
        }

        public void UpdateReview(ReviewDto updatedReviewDto)
        {
            bool exists = this.DbContext.Reviews.Any(b => b.Id == updatedReviewDto.Id);
            if (!exists) throw new ArgumentException("Such review doesn't exist!");

            Review review = this.Mapper.Map<ReviewDto, Review>(updatedReviewDto);

            this.DbContext.Reviews.Update(review);
            this.DbContext.SaveChanges();
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
