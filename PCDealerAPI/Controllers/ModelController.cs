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
            ModelDto[] brandModels = this.ModelService.GetAllBrandModels(brandId);

            return Ok(brandModels);
        }

        [HttpGet]
        [EnableCors("MyCorsPolicy")]
        [Route("{brandId}/get/{modelId}")]
        public IActionResult GetModel([FromRoute] string brandId, [FromRoute] string modelId)
        {
            ModelDto brandModel = this.ModelService.GetModel(brandId, modelId);

            return Ok(brandModel);
        }

        [HttpPost]
        [EnableCors("MyCorsPolicy")]
        [Route("{brandId}/add")]
        public IActionResult AddModel([FromRoute] string brandId, [FromForm] ModelDto model)
        {
            try
            {
                this.ModelService.AddModel(brandId, model);

                return Ok(model);

            }
            catch (ArgumentNullException)
            {
                return NotFound("Such brand doesn't exist!");
            }
        }

        [HttpPut]
        [EnableCors("MyCorsPolicy")]
        [Route("update/{modelId}")]
        public IActionResult UpdateModel(string modelId, [FromForm] ModelDto model)
        {
            try
            {
                model.Id = modelId;
                this.ModelService.UpdateModel(modelId, model);

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
            catch (ArgumentNullException)
            {
                return NotFound("Such model doesn't exist!");
            }
        }
    }
}
