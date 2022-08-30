namespace Data.Services.EntityServices
{
    using AutoMapper;

    using Constants;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ModelService : IModelService
    {
        public ModelService(PcDealerDbContext dbContext, IMapper mapper, IBrandService brandService, ICategoryService categoryService)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
            this.BrandService = brandService;
            this.CategoryService = categoryService;
        }

        public PcDealerDbContext DbContext { get; set; }

        public IMapper Mapper { get; set; }

        public IBrandService BrandService { get; init; }

        public ICategoryService CategoryService { get; init; }

        public ICollection<ModelDto> GetAllBrandModels(string brandId)
        {
            Brand brand = this.DbContext.Brands.AsNoTracking()
                                                .FirstOrDefault(b => b.Id == brandId && b.IsDeleted == false);

            ICollection<ModelDto> brandModelDtos = new List<ModelDto>();

            if (brand is null) throw new ArgumentException(ErrorMessage.NON_EXISTING_BRAND_MESSAGE);

            Model[] brandModels = this.DbContext.Models.Where(m => m.Brand == brand && m.IsDeleted == false).ToArray();
            brandModelDtos = this.Mapper.Map<ICollection<Model>, ICollection<ModelDto>>(brandModels);

            return brandModelDtos;
        }

        public ModelDto GetModel(string modelId)
        {
            Model brandModel = this.DbContext.Models.AsNoTracking()
                                                    .Include(m => m.Brand).Where(m => m.Brand.IsDeleted == false)
                                                    .Include(m => m.Category).Where(m => m.Category.IsDeleted == false)
                                                    .FirstOrDefault(m => m.Id == modelId && m.IsDeleted == false);
            ModelDto brandModelDto = this.Mapper.Map<Model, ModelDto>(brandModel);

            return brandModelDto;
        }

        public void AddModel(ModelDto modelDto, string brandId, string categoryId)
        {
            bool brandExists = this.DbContext.Brands.AsNoTracking().Any(b => b.Id == brandId && b.IsDeleted == false);
            if (!brandExists) throw new ArgumentException(ErrorMessage.NON_EXISTING_BRAND_MESSAGE);

            bool categoryExists = this.DbContext.Categories.AsNoTracking().Any(c => c.Id == categoryId && c.IsDeleted == false);
            if (categoryId is not null)
            {
                if (!categoryExists) throw new ArgumentException(ErrorMessage.NON_EXISTING_CATEGORY_MESSAGE);

                modelDto.Brand = this.BrandService.GetBrandByBrandId(brandId);
                modelDto.Category = this.CategoryService.GetCategory(categoryId);
            } 

            Model model = this.Mapper.Map<ModelDto, Model>(modelDto);
            this.DbContext.Brands.Attach(model.Brand);
            this.DbContext.Categories.Attach(model.Category);
            this.DbContext.Models.Add(model);

            this.DbContext.SaveChanges();

            modelDto.CreatedAtUtc = model.CreatedAtUtc;
            modelDto.ModifiedAtUtc = model.CreatedAtUtc;
        }

        public void UpdateModel(ModelDto modelDto, string categoryId)
        {
            bool modelExists = this.DbContext.Models.Any(m => m.Id == modelDto.Id && m.IsDeleted == false);
            if (!modelExists) throw new ArgumentException(ErrorMessage.NON_EXISTING_MODEL_MESSAGE);

            bool categoryExists = this.DbContext.Categories.Any(c => c.Id == categoryId && c.IsDeleted == false);

            if (categoryId is null)
            {
                categoryId = this.DbContext.Models.Where(m => m.Id == modelDto.Id && m.IsDeleted == false)
                                                    .AsNoTracking()
                                                    .Include(m => m.Category)
                                                    .First().Category.Id;
                modelDto.Category = this.CategoryService.GetCategory(modelDto.Id);
            }

            CategoryDto categoryDto = this.CategoryService.GetCategory(categoryId);
            if (categoryDto is null) throw new ArgumentException(ErrorMessage.NON_EXISTING_CATEGORY_MESSAGE);

            modelDto.Category = categoryDto;
            modelDto.Brand = this.BrandService.GetBrandByModelId(modelDto.Id);

            Model model = this.Mapper.Map<ModelDto, Model>(modelDto);
            this.DbContext.Models.Update(model);

            this.DbContext.SaveChanges();

            modelDto.CreatedAtUtc = model.CreatedAtUtc;
            modelDto.ModifiedAtUtc = model.ModifiedAtUtc;
        }

        public void DeleteModel(string modelId)
        {
            bool exists = this.DbContext.Models.Any(st => st.Id == modelId);
            if (!exists) throw new ArgumentException(ErrorMessage.NON_EXISTING_MODEL_MESSAGE);

            Model modelToBeDeleted = this.DbContext.Models.Where(m => m.Id == modelId).FirstOrDefault();
            modelToBeDeleted.IsDeleted = true;
            modelToBeDeleted.DeletedAtUtc = DateTime.UtcNow;

            this.DbContext.SaveChanges();
        }
    }
}