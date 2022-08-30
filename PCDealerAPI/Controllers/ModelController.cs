namespace PCDealerAPI.Controllers
{
    using Constants;

    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    [Route(ControllerConstant.CONTROLLER_BASE_ROUTE)]
    [ApiController]
    public class ModelController : ControllerBase
    {
        public ModelController(IModelService modelService)
        {
            this.ModelService = modelService;
        }

        public IModelService ModelService { get; set; }

        [HttpGet]
        [Route($"{ControllerConstant.BRAND_ID_PARAMETER}/{ControllerConstant.ALL_ROUTE}")]
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
        [Route(ControllerConstant.MODEL_ID_PARAMETER)]
        public IActionResult GetModel([FromRoute] string modelId)
        {
            ModelDto brandModel = this.ModelService.GetModel(modelId);

            if (brandModel is null) return NotFound(ErrorMessage.NON_EXISTING_MODEL_MESSAGE);

            return Ok(brandModel);
        }

        [HttpPost]
        [Route(
            $"{ControllerConstant.BRAND_ID_PARAMETER}/" +
            $"{ControllerConstant.CATEGORY_ID_OPTIONAL_PARAMETER}/" +
            $"{ControllerConstant.ADD_ROUTE}"
        )]
        public IActionResult AddModel([FromRoute] string brandId, [FromRoute] string? categoryId, [FromForm] ModelDto model)
        {
            try
            {
                this.ModelService.AddModel(model, brandId, categoryId);

                return Ok(model);

            }
            catch (NullReferenceException)
            {
                return NotFound(ErrorMessage.NON_EXISTING_BRAND_MESSAGE);
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }

        [HttpPut]
        [Route(
            $"{ControllerConstant.UPDATE_ROUTE}" +
            $"{ControllerConstant.MODEL_ID_PARAMETER}/" +
            $"{ControllerConstant.CATEGORY_ID_OPTIONAL_PARAMETER}"
        )]
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
                return NotFound(ErrorMessage.NON_EXISTING_BRAND_OR_MODEL);
            }
        }

        [HttpDelete]
        [Route(ControllerConstant.DELETE_ROUTE + ControllerConstant.MODEL_ID_PARAMETER)]
        public IActionResult DeleteModel(string modelId)
        {
            try
            {
                this.ModelService.DeleteModel(modelId);

                return Ok(InfoMessage.MODEL_SUCCESSFULLY_DELETED_MESSAGE);
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }
    }
}
