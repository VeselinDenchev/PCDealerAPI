namespace PCDealerAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public BrandController(IBrandService brandService)
        {
            this.BrandService = brandService;
        }

        public IBrandService BrandService { get; set; }

        [HttpGet]
        [EnableCors("MyCorsPolicy")]
        [Route("get/all")]
        public IActionResult GetAllBrands()
        {
            BrandDto[] brands = this.BrandService.GetAllBrands();

            return Ok(brands);
        }

        [HttpGet]
        [EnableCors("MyCorsPolicy")]
        [Route("get/{brandId}")]
        public IActionResult GetBrand([FromRoute] string brandId)
        {
            BrandDto brand = this.BrandService.GetBrand(brandId);

            return Ok(brand);
        }

        [HttpPost]
        [EnableCors("MyCorsPolicy")]
        [Route("add")]
        public IActionResult AddBrand([FromForm] BrandDto brand)
        {
            this.BrandService.AddBrand(brand);

            return Ok(brand);
        }

        [HttpPut]
        [EnableCors("MyCorsPolicy")]
        [Route("update/{brandId}")]
        public IActionResult UpdateBrand([FromRoute] string brandId, [FromForm] BrandDto brand)
        {
            brand.Id = brandId;
            this.BrandService.UpdateBrand(brandId,brand);

            return Ok(brand);
        }

        [HttpDelete]
        [Route("delete/{brandId}")]
        public IActionResult DeleteBrand([FromRoute] string brandId)
        {
            if (brandId is null)
            {
                return NotFound("Region not found!");
            }

            this.BrandService.DeleteBrand(brandId);

            return Ok(JsonConvert.SerializeObject("Brand successfully deleted!"));
        }
    }
}
