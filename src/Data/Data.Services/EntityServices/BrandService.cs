namespace Data.Services.EntityServices
{
    using System;
    using System.Linq;

    using AutoMapper;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class BrandService : IBrandService
    {
        public BrandService(PcDealerDbContext dbContext, IMapper mapper, IModelService modelService)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
            this.ModelService = modelService;
        }

        public PcDealerDbContext DbContext { get; init; }

        public IMapper Mapper { get; init; }

        public IModelService ModelService { get; init; }

        public BrandDto[] GetAllBrands()
        {
            var brands = this.DbContext.Brands
                                            .Where(b => b.IsDeleted == false)
                                            .Include(b => b.Models)
                                            .ToArray();
            var brandDtos = this.Mapper.Map<Brand[], BrandDto[]>(brands);

            return brandDtos;
        }

        public BrandDto GetBrand(string brandId)
        {
            Brand brand = this.DbContext.Brands.Where(r => r.Id == brandId && r.IsDeleted == false)
                                                .Include(b => b.Models)
                                                .FirstOrDefault();
            BrandDto brandDto = this.Mapper.Map<Brand, BrandDto>(brand);

            return brandDto;
        }

        public void AddBrand(BrandDto brandDto)
        {
            Brand brand = this.Mapper.Map<BrandDto, Brand>(brandDto);
            brandDto.Id = brand.Id;

            this.DbContext.Brands.Add(brand);
            this.DbContext.SaveChanges();
        }

        public void UpdateBrand(string brandId, BrandDto updatedBrandDto)
        {
            ICollection<ModelDto> brandModelDtos = this.ModelService.GetAllBrandModels(brandId);
            updatedBrandDto.Models = brandModelDtos;

            ICollection<Model> brandModels = this.Mapper.Map<ICollection<ModelDto>, ICollection<Model>>(brandModelDtos);

            Brand brand = this.Mapper.Map<BrandDto, Brand>(updatedBrandDto);
            brand.Models = brandModels;

            this.DbContext.Brands.Update(brand);
            this.DbContext.SaveChanges();
        }

        public void DeleteBrand(string brandId)
        {
            Brand brand = this.DbContext.Brands.Where(r => r.Id == brandId && r.IsDeleted == false)
                                                .Include(b => b.Models)
                                                .FirstOrDefault();

            brand.IsDeleted = true;
            brand.DeletedAtUtc = DateTime.UtcNow;

            foreach (Model model in brand.Models)
            {
                model.IsDeleted = true;
                model.DeletedAtUtc = DateTime.UtcNow;
            }

            this.DbContext.Brands.Update(brand);
            this.DbContext.Models.UpdateRange(brand.Models);
            this.DbContext.SaveChanges();
        }
    }
}
