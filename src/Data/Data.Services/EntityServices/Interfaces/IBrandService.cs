namespace Data.Services.EntityServices.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Data.Services.DtoModels;

    public interface IBrandService
    {
        public BrandDto[] GetAllBrands();

        public BrandDto GetBrand(string brandId);

        public void AddBrand(BrandDto brandDto);

        public void UpdateBrand(string brandId, BrandDto updatedBrandDto);

        public void DeleteBrand(string brandId);
    }
}
