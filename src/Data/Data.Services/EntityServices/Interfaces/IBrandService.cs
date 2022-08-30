namespace Data.Services.EntityServices.Interfaces
{
    using Data.Services.DtoModels;

    public interface IBrandService
    {
        public BrandDto[] GetAllBrands();

        public BrandDto GetBrandByModelId(string modelId);

        public BrandDto GetBrandByBrandId(string brandId);

        public void AddBrand(BrandDto brandDto);

        public void UpdateBrand(BrandDto updatedBrandDto);

        public void DeleteBrand(string brandId);
    }
}
