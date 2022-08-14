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

        public ModelDto[] GetAllBrandModels(string brandId)
        {
            Model[] brandModels = this.DbContext.Brands.Include(b => b.Models)
                                                        .FirstOrDefault(b => b.Id == brandId && b.IsDeleted == false)
                                                        .Models.Where(m => m.IsDeleted == false)
                                                                .ToArray();
            ModelDto[] brandModelDtos = this.Mapper.Map<Model[], ModelDto[]>(brandModels);

            return brandModelDtos;
        }

        public ModelDto GetModel(string brandId, string modelId)
        {
            Model brandModel = this.DbContext.Brands.Include(b => b.Models)
                                                    .FirstOrDefault(b => b.Id == brandId && b.IsDeleted == false)
                                                    .Models.FirstOrDefault(m => m.Id == modelId && m.IsDeleted == false);
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

        public void UpdateModel(string modelId, ModelDto modelDto)
        {
            Model model = this.Mapper.Map<ModelDto, Model>(modelDto);
            //model.Id = modelId;
            this.DbContext.Models.Update(model);

            this.DbContext.SaveChanges();
        }

        public void DeleteModel(string modelId)
        {
            Model modelToBeDeleted = this.DbContext.Models.Where(m => m.Id == modelId).FirstOrDefault();
            modelToBeDeleted.IsDeleted = true;
            modelToBeDeleted.DeletedAtUtc = DateTime.UtcNow;

            this.DbContext.SaveChanges();
        }
    }
}
