namespace PCDealerAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;


    [Route("api/[controller]")]
    [ApiController]
    public class SpecificationController : ControllerBase
    {
        public SpecificationController(ISpecificationService specificationService)
        {
            this.SpecificationService = specificationService;
        }

        public ISpecificationService SpecificationService { get; set; }

        /*[HttpGet]
        [Route("{brandId}/all")]
        public IActionResult GetAllBrandSpecifications([FromRoute] string brandId)
        {
            try
            {
                ICollection<SpecificationDto> brandSpecifications = this.SpecificationService.GetAllBrandSpecifications(brandId);

                return Ok(brandSpecifications);

            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }*/

        /*[HttpGet]
        [EnableCors("MyCorsPolicy")]
        [Route("get/{specificationId}")]
        public IActionResult GetSpecification([FromRoute] string specificationId)
        {
            SpecificationDto brandSpecification = this.SpecificationService.GetSpecification(specificationId);
            if (brandSpecification is null) return NotFound("Can't find such specification!");

            return Ok(brandSpecification);
        }*/

        [HttpPost]
        [EnableCors("MyCorsPolicy")]
        [Route("specificationType/{specificationTypeId}/add")]
        public IActionResult AddSpecification([FromRoute] string specificationTypeId, [FromForm] SpecificationDto specification)
        {
            try
            {
                this.SpecificationService.AddSpecification(specificationTypeId, specification);

                return Ok(specification);

            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }

        [HttpPut]
        [EnableCors("MyCorsPolicy")]
        [Route("update/{specificationId}/{specificationTypeId?}")]
        public IActionResult UpdateSpecification([FromRoute] string specificationId, [FromForm] SpecificationDto specification,
                                                [FromRoute] string? specificationTypeId)
        {
            try
            {
                specification.Id = specificationId;

                this.SpecificationService.UpdateSpecification(specification, specificationTypeId);

                return Ok(specification);

            }
            catch (ArgumentException)
            {
                return NotFound("Such specification or/and specification type doesn't/don't exist!");
            }
        }

        [HttpDelete]
        [EnableCors("MyCorsPolicy")]
        [Route("delete/{specificationId}")]
        public IActionResult DeleteSpecification([FromRoute] string specificationId)
        {
            try
            {
                this.SpecificationService.DeleteSpecification(specificationId);

                return Ok("The specification is successfully deleted");
            }
            catch (ArgumentNullException)
            {
                return NotFound("Such specification doesn't exist!");
            }
        }
    }
}
