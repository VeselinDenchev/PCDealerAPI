namespace Data.Services.EntityServices
{
    using System;
    using System.Linq;

    using AutoMapper;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    public class BrandService : IBrandService
    {
        public BrandService(PcDealerDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public PcDealerDbContext DbContext { get; set; }

        public IMapper Mapper { get; set; }

        public BrandDto[] GetAllBrands()
        {
            var brands = this.DbContext.Brands
                                            .Where(b => b.IsDeleted == false)
                                            .ToArray();
            var brandDtos = this.Mapper.Map<Brand[], BrandDto[]>(brands);

            return brandDtos;
        }

        public BrandDto GetBrand(string brandId)
        {
            Brand brand = this.DbContext.Brands.Where(r => r.Id == brandId && r.IsDeleted == false).FirstOrDefault();
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
            Brand brand = this.Mapper.Map<BrandDto, Brand>(updatedBrandDto);
            brand.Id = brandId;

            this.DbContext.Brands.Update(brand);
            this.DbContext.SaveChanges();
        }

        public void DeleteBrand(string brandId)
        {
            Brand brand = this.DbContext.Brands.Where(r => r.Id == brandId && r.IsDeleted == false).FirstOrDefault();

            brand.IsDeleted = true;
            brand.DeletedAtUtc = DateTime.UtcNow;

            this.DbContext.Brands.Update(brand);
            this.DbContext.SaveChanges();
        }
    }
}
