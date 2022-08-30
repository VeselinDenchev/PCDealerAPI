namespace PCDealerAPI.Controllers
{
    using Constants;

    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    [Route(ControllerConstant.CONTROLLER_BASE_ROUTE)]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public CategoryController(ICategoryService categoryService)
        {
            this.CategoryService = categoryService;
        }

        public ICategoryService CategoryService { get; init; }

        [HttpPost]
        [Route(ControllerConstant.ADD_ROUTE)]
        public IActionResult AddCategory([FromForm] CategoryDto category)
        {
            this.CategoryService.AddCategory(category);

            return Ok(category);
        }

        [HttpPut]
        [Route(ControllerConstant.UPDATE_ROUTE + ControllerConstant.CATEGORY_ID_PARAMETER)]
        public IActionResult UpdateCategory(string categoryId, [FromForm] CategoryDto category)
        {
            try
            {
                category.Id = categoryId;
                this.CategoryService.UpdateCategory(category);

                return Ok(category);

            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }

        [HttpDelete]
        [Route(ControllerConstant.DELETE_ROUTE + ControllerConstant.CATEGORY_ID_PARAMETER)]
        public IActionResult DeleteCategory(string categoryId)
        {
            try
            {
                this.CategoryService.DeleteCategory(categoryId);

                return Ok(InfoMessage.CATEGORY_SUCCESSFULLY_DELETED_MESSAGE);
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }
    }
}
