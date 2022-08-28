namespace Data.Services.EntityServices
{
    using AutoMapper;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        public CategoryService(PcDealerDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public PcDealerDbContext DbContext { get; init; }

        public IMapper Mapper { get; init; }

        public CategoryDto GetCategory(string categoryId)
        {
            Category category = this.DbContext.Categories.Where(c => c.Id == categoryId && c.IsDeleted == false)
                                                            .AsNoTracking()
                                                            .FirstOrDefault();
            CategoryDto categoryDto = this.Mapper.Map<Category, CategoryDto>(category);

            return categoryDto;
        }

        public void AddCategory(CategoryDto categoryDto)
        {
            Category category = this.Mapper.Map<CategoryDto, Category>(categoryDto);
            
            this.DbContext.Categories.Add(category);

            this.DbContext.SaveChanges();

            categoryDto.CreatedAtUtc = category.CreatedAtUtc;
            categoryDto.ModifiedAtUtc = category.ModifiedAtUtc;
        }

        public void UpdateCategory(CategoryDto categoryDto)
        {
            bool exists = this.DbContext.Categories.Any(c => c.Id == categoryDto.Id);
            if (!exists) throw new ArgumentException("Such category doesn't exist!");

            Category category = this.Mapper.Map<CategoryDto, Category>(categoryDto);
            this.DbContext.Categories.Update(category);

            this.DbContext.SaveChanges();

            categoryDto.CreatedAtUtc = category.CreatedAtUtc;
            categoryDto.ModifiedAtUtc = category.ModifiedAtUtc;
        }

        public void DeleteCategory(string categoryId)
        {
            bool exists = this.DbContext.Categories.Any(c => c.Id == categoryId);
            if (!exists) throw new ArgumentException("Such category doesn't exist!");

            Category categoryToBeDeleted = this.DbContext.Categories.Where(m => m.Id == categoryId).First();
            categoryToBeDeleted.IsDeleted = true;
            categoryToBeDeleted.DeletedAtUtc = DateTime.UtcNow;

            this.DbContext.SaveChanges();
        }
    }
}