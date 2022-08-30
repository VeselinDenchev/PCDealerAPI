namespace PCDealerAPI.Controllers
{
    using Constants;

    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    [Route(ControllerConstant.CONTROLLER_BASE_ROUTE)]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public BrandController(IBrandService brandService)
        {
            this.BrandService = brandService;
        }

        public IBrandService BrandService { get; set; }

        [HttpGet]
        [Route(ControllerConstant.ALL_ROUTE)]
        public IActionResult GetAllBrands()
        {
            BrandDto[] brands = this.BrandService.GetAllBrands();

            return Ok(brands);
        }

        [HttpGet]
        [Route(ControllerConstant.BRAND_ID_PARAMETER)]
        public IActionResult GetBrand([FromRoute] string brandId)
        {
            BrandDto brand = this.BrandService.GetBrandByBrandId(brandId);

            if (brand is null) return NotFound(ErrorMessage.NON_EXISTING_BRAND_MESSAGE);

            return Ok(brand);
        }

        [HttpPost]
        [Route(ControllerConstant.ADD_ROUTE)]
        public IActionResult AddBrand([FromForm] BrandDto brand)
        {
            this.BrandService.AddBrand(brand);

            return Ok(brand);
        }

        [HttpPut]
        [Route(ControllerConstant.UPDATE_ROUTE + ControllerConstant.BRAND_ID_PARAMETER)]
        public IActionResult UpdateBrand([FromRoute] string brandId, [FromForm] BrandDto brand)
        {
            try
            {
                brand.Id = brandId;
                this.BrandService.UpdateBrand(brand);

                return Ok(brand);
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }


        }

        [HttpDelete]
        [Route(ControllerConstant.DELETE_ROUTE + ControllerConstant.BRAND_ID_PARAMETER)]
        public IActionResult DeleteBrand([FromRoute] string brandId)
        {
            try
            {
                this.BrandService.DeleteBrand(brandId);

                return Ok(InfoMessage.BRAND_SUCCESSFULLY_DELETED_MESSAGE);

            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }
    }
}
