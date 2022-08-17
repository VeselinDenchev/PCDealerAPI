namespace PCDealerAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        public ModelController(IModelService modelService)
        {
            this.ModelService = modelService;
        }

        public IModelService ModelService { get; set; }

        [HttpGet]
        [EnableCors("MyCorsPolicy")]
        [Route("{brandId}/all")]
        public IActionResult GetAllBrandModels([FromRoute] string brandId)
        {
            try
            {
                ICollection<ModelDto> brandModels = this.ModelService.GetAllBrandModels(brandId);

                return Ok(brandModels);

            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }

        [HttpGet]
        [EnableCors("MyCorsPolicy")]
        [Route("{modelId}")]
        public IActionResult GetModel([FromRoute] string modelId)
        {
            ModelDto brandModel = this.ModelService.GetModel(modelId);

            if (brandModel is null) return NotFound("Such model doesn't exist!");

            return Ok(brandModel);
        }

        [HttpPost]
        [EnableCors("MyCorsPolicy")]
        [Route("{brandId}/{categoryId?}/add")]
        public IActionResult AddModel([FromRoute] string brandId, [FromRoute] string? categoryId, [FromForm] ModelDto model)
        {
            try
            {
                this.ModelService.AddModel(model, brandId, categoryId);

                return Ok(model);

            }
            catch (NullReferenceException)
            {
                return NotFound("Such brand doesn't exist!");
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }

        [HttpPut]
        [EnableCors("MyCorsPolicy")]
        [Route("update/{modelId}/{categoryId?}")]
        public IActionResult UpdateModel([FromRoute] string modelId, [FromRoute] string? categoryId, [FromForm] ModelDto model)
        {
            try
            {
                model.Id = modelId;
                this.ModelService.UpdateModel(model, categoryId);

                return Ok(model);

            }
            catch (ArgumentNullException)
            {
                return NotFound("Such brand or model doesn't exist!");
            }
        }

        [HttpDelete]
        [EnableCors("MyCorsPolicy")]
        [Route("delete/{modelId}")]
        public IActionResult DeleteModel(string modelId)
        {
            try
            {
                this.ModelService.DeleteModel(modelId);

                return Ok("The model is successfully deleted");
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }
    }
}
