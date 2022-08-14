namespace PCDealerAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class SpecificationTypeController : ControllerBase
    {
        public SpecificationTypeController(ISpecificationTypeService specificationTypeService)
        {
            this.SpecificationTypeService = specificationTypeService;
        }

        public ISpecificationTypeService SpecificationTypeService { get; set; }

        [HttpGet]
        [EnableCors("MyCorsPolicy")]
        [Route("get/all")]
        public IActionResult GetAllSpecificationTypes()
        {
            ICollection<SpecificationTypeDto> specificationTypes = this.SpecificationTypeService.GetAllSpecificationTypes();

            return Ok(specificationTypes);
        }

        [HttpPost]
        [EnableCors("MyCorsPolicy")]
        [Route("add")]
        public IActionResult AddSpecificationType([FromForm] SpecificationTypeDto specificationType)
        {
            this.SpecificationTypeService.AddSpecificationType(specificationType);

            return Ok(specificationType);
        }

        [HttpPut]
        [EnableCors("MyCorsPolicy")]
        [Route("update/{specificationTypeId}")]
        public IActionResult UpdateSpecificationType(string specificationTypeId, [FromForm] SpecificationTypeDto specificationType)
        {
            try
            {
                specificationType.Id = specificationTypeId;
                this.SpecificationTypeService.UpdateSpecificationType(specificationType);

                return Ok(specificationType);

            }
            catch (ArgumentNullException)
            {
                return NotFound("Such specification type doesn't exist!");
            }
        }

        [HttpDelete]
        [EnableCors("MyCorsPolicy")]
        [Route("delete/{specificationTypeId}")]
        public IActionResult DeleteSpecificationType(string specificationTypeId)
        {
            try
            {
                this.SpecificationTypeService.DeleteSpecificationType(specificationTypeId);

                return Ok("The specification type is successfully deleted");
            }
            catch (ArgumentNullException)
            {
                return NotFound("Such specification type doesn't exist!");
            }
        }
    }
}
