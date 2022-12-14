namespace Data.Services.EntityServices
{
    using AutoMapper;

    using Constants;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ReviewService : IReviewService
    {
        public ReviewService(PcDealerDbContext dbContext, IMapper mapper, IProductService productService)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
            this.ProductService = productService;
        }

        public PcDealerDbContext DbContext { get; init; }

        public IMapper Mapper { get; init; }

        public IProductService ProductService { get; init; }

        public ReviewDto[] GetAllReviewsForProduct(string productId)
        {
            bool exists = this.DbContext.Products.Any(p => p.Id == productId && p.IsDeleted == false);
            if (!exists) throw new ArgumentException(ErrorMessage.NON_EXISTING_PRODUCT_MESSAGE);

            Review[] reviews = this.DbContext.Reviews.Where(r => r.Product.Id == productId && r.IsDeleted == false)
                                                        .Include(r => r.User)
                                                        .Include(r => r.Product)
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

        public void AddReview(ReviewDto reviewDto, string productId, string userName)
        {
            bool exists = this.DbContext.Products.AsNoTracking().Any(p => p.Id == productId && p.IsDeleted == false);
            if (!exists) throw new ArgumentException(ErrorMessage.NON_EXISTING_PRODUCT_MESSAGE);


            User user = this.DbContext.Users.Where(u => u.UserName == userName).First();


            reviewDto.User = user;
            reviewDto.Product = this.ProductService.GetProduct(productId);

            Review review = this.Mapper.Map<ReviewDto, Review>(reviewDto);

            this.DbContext.Entry(review.Product).State = EntityState.Detached;

            this.DbContext.Brands.Attach(review.Product.Model.Brand);
            this.DbContext.Categories.Attach(review.Product.Model.Category);
            this.DbContext.Models.Attach(review.Product.Model);
            this.DbContext.Products.Attach(review.Product);

            this.DbContext.Reviews.Add(review);

            this.DbContext.SaveChanges();

            reviewDto.CreatedAtUtc = DateTime.UtcNow;
            reviewDto.ModifiedAtUtc = DateTime.UtcNow;
        }

        public void UpdateReview(ReviewDto updatedReviewDto, string userName)
        {
            bool exists = this.DbContext.Reviews.Any(b => b.Id == updatedReviewDto.Id);
            if (!exists) throw new ArgumentException(ErrorMessage.NON_EXISTING_REVIEW_MESSAGE);

            string ownerUserName = this.DbContext.Reviews.Where(r => r.Id == updatedReviewDto.Id && r.IsDeleted == false)
                                                            .AsNoTracking()
                                                            .Include(r => r.User)
                                                            .First().User.UserName;

            bool isOwnedByThisUser = userName == ownerUserName;
            if (!isOwnedByThisUser) throw new UnauthorizedAccessException(ErrorMessage.REVIEW_UNAUTHORIZED_ACCESS_MESSAGE);

            DateTime createdAtUtc = this.DbContext.Reviews.Where(r => r.Id == updatedReviewDto.Id && r.IsDeleted == false)
                                                            .AsNoTracking()
                                                            .First().CreatedAtUtc;
            updatedReviewDto.CreatedAtUtc = createdAtUtc;

            User user = this.DbContext.Users.Where(u => u.UserName == userName).AsNoTracking().First();
            updatedReviewDto.User = user;

            Review review = this.Mapper.Map<ReviewDto, Review>(updatedReviewDto);

            this.DbContext.Reviews.Update(review);
            this.DbContext.SaveChanges();

            updatedReviewDto.ModifiedAtUtc = DateTime.UtcNow;
        }

        public void DeleteReview(string reviewId)
        {
            bool exists = this.DbContext.Reviews.Any(b => b.Id == reviewId);
            if (!exists) throw new ArgumentException(ErrorMessage.NON_EXISTING_REVIEW_MESSAGE);

            Review review = this.DbContext.Reviews.Where(r => r.Id == reviewId && r.IsDeleted == false).First();

            review.IsDeleted = true;
            review.DeletedAtUtc = DateTime.UtcNow;

            this.DbContext.Reviews.Update(review);
            this.DbContext.SaveChanges();
        }
    }
}