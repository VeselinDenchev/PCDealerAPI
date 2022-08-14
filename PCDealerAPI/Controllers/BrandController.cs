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

            if (brand is null) return NotFound("Such brand doesn't exist!");

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
        [Route("delete/{brandId}")]
        public IActionResult DeleteBrand([FromRoute] string brandId)
        {
            try
            {
                this.BrandService.DeleteBrand(brandId);

                return Ok("Brand successfully deleted!");

            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }

        }
    }
}
