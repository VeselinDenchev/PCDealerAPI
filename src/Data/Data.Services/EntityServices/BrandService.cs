namespace Data.Services.EntityServices
{
    using System;
    using System.Linq;

    using AutoMapper;

    using Constants;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class BrandService : IBrandService
    {
        public BrandService(PcDealerDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public PcDealerDbContext DbContext { get; init; }

        public IMapper Mapper { get; init; }

        public BrandDto[] GetAllBrands()
        {
            var brands = this.DbContext.Brands.Where(b => b.IsDeleted == false).ToArray();
            var brandDtos = this.Mapper.Map<Brand[], BrandDto[]>(brands);

            foreach (BrandDto brand in brandDtos)
            {
                brand.ProductsCount = this.GetBrandProductsCount(brand.Id);
            }

            return brandDtos;
        }

        public BrandDto GetBrandByModelId(string modelId)
        {
            Brand brand = this.DbContext.Models.Where(m => m.Id == modelId && m.IsDeleted == false)
                                                .AsNoTracking()
                                                .Include(m => m.Brand)
                                                .First()
                                                .Brand;
            BrandDto brandDto = this.Mapper.Map<Brand, BrandDto>(brand);

            return brandDto;
        }

        public BrandDto GetBrandByBrandId(string brandId)
        {
            Brand brand = this.DbContext.Brands.Where(r => r.Id == brandId && r.IsDeleted == false)
                                                .AsNoTracking()
                                                .FirstOrDefault();
            BrandDto brandDto = this.Mapper.Map<Brand, BrandDto>(brand);

            return brandDto;
        }

        public void AddBrand(BrandDto brandDto)
        {
            Brand brand = this.Mapper.Map<BrandDto, Brand>(brandDto);

            this.DbContext.Brands.Add(brand);
            this.DbContext.SaveChanges();

            brandDto.CreatedAtUtc = brand.CreatedAtUtc;
            brandDto.ModifiedAtUtc = brand.ModifiedAtUtc;
            brandDto.ProductsCount = 0;
        }

        public void UpdateBrand(BrandDto updatedBrandDto)
        {
            bool exists = this.DbContext.Brands.Any(b => b.Id == updatedBrandDto.Id);
            if (!exists) throw new ArgumentException(ErrorMessage.NON_EXISTING_BRAND_MESSAGE);

            Brand brand = this.Mapper.Map<BrandDto, Brand>(updatedBrandDto);

            this.DbContext.Brands.Update(brand);
            this.DbContext.SaveChanges();

            updatedBrandDto.CreatedAtUtc = brand.CreatedAtUtc;
            updatedBrandDto.ModifiedAtUtc = brand.ModifiedAtUtc;
        }

        public void DeleteBrand(string brandId)
        {
            bool exists = this.DbContext.Brands.Any(b => b.Id == brandId);
            if (!exists) throw new ArgumentException(ErrorMessage.NON_EXISTING_BRAND_MESSAGE);

            Brand brand = this.DbContext.Brands.Where(r => r.Id == brandId && r.IsDeleted == false)
                                                //.Include(b => b.Models)
                                                .First();

            brand.IsDeleted = true;
            brand.DeletedAtUtc = DateTime.UtcNow;

            Model[] brandModels = this.DbContext.Models.Where(m => m.Brand == brand).ToArray();

            foreach (Model model in brandModels)
            {
                model.IsDeleted = true;
                model.DeletedAtUtc = DateTime.UtcNow;
            }

            this.DbContext.Brands.Update(brand);
            this.DbContext.Models.UpdateRange(brandModels);
            this.DbContext.SaveChanges();
        }

        private int GetBrandProductsCount(string brandId)
        {
            int BrandProductsCount = this.DbContext.Products.Where(p => p.IsDeleted == false && p.Model.IsDeleted == false
                                                                    && p.Model.Brand.IsDeleted == false)
                                                                .Include(p => p.Model).ThenInclude(m => m.Brand)
                                                                .Count(p => p.Model.Brand.Id == brandId);

            return BrandProductsCount;
        }
    }
}