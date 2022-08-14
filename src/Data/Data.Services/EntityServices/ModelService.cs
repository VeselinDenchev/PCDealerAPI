namespace Data.Services.EntityServices
{
    using AutoMapper;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ModelService : IModelService
    {
        public ModelService(PcDealerDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public PcDealerDbContext DbContext { get; set; }

        public IMapper Mapper { get; set; }

        public ICollection<ModelDto> GetAllBrandModels(string brandId)
        {
            Brand brand = this.DbContext.Brands.Include(b => b.Models)
                                                        .AsNoTracking()
                                                        .FirstOrDefault(b => b.Id == brandId && b.IsDeleted == false);

            ICollection<ModelDto> brandModelDtos = new List<ModelDto>();

            if (brand is null) throw new ArgumentException("Invalid brand!");

            var brandModels = brand.Models.Where(m => m.IsDeleted == false).ToArray();
            brandModelDtos = this.Mapper.Map<ICollection<Model>, ICollection<ModelDto>>(brandModels);

            return brandModelDtos;
        }

        public ModelDto GetModel(string modelId)
        {
            Model brandModel = this.DbContext.Models.FirstOrDefault(m => m.Id == modelId && m.IsDeleted == false);
            ModelDto brandModelDto = this.Mapper.Map<Model, ModelDto>(brandModel);

            return brandModelDto;
        }

        public void AddModel(string brandId, ModelDto modelDto)
        {
            Model model = this.Mapper.Map<ModelDto, Model>(modelDto);
            this.DbContext.Models.Add(model);
            this.DbContext.Brands.Include(b => b.Models).FirstOrDefault(b => b.Id == brandId && b.IsDeleted == false)
                                                        .Models.Add(model);
            this.DbContext.SaveChanges();
        }

        public void UpdateModel(ModelDto modelDto)
        {
            bool exists = this.DbContext.Models.Any(st => st.Id == modelDto.Id);
            if (!exists) throw new ArgumentException("Such model doesn't exist!");

            Model model = this.Mapper.Map<ModelDto, Model>(modelDto);
            this.DbContext.Models.Update(model);

            this.DbContext.SaveChanges();
        }

        public void DeleteModel(string modelId)
        {
            bool exists = this.DbContext.Models.Any(st => st.Id == modelId);
            if (!exists) throw new ArgumentException("Such model doesn't exist!");

            Model modelToBeDeleted = this.DbContext.Models.Where(m => m.Id == modelId).FirstOrDefault();
            modelToBeDeleted.IsDeleted = true;
            modelToBeDeleted.DeletedAtUtc = DateTime.UtcNow;

            this.DbContext.SaveChanges();
        }
    }
}
