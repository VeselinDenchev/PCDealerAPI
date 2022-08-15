namespace PCDealerAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public CategoryController(ICategoryService categoryService)
        {
            this.CategoryService = categoryService;
        }

        public ICategoryService CategoryService { get; init; }

        // TODO: Get category for product

        [HttpPost]
        [EnableCors("MyCorsPolicy")]
        [Route("add")]
        public IActionResult AddCategory([FromForm] CategoryDto category)
        {
            this.CategoryService.AddCategory(category);

            return Ok(category);
        }

        [HttpPut]
        [EnableCors("MyCorsPolicy")]
        [Route("update/{categoryId}")]
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
        [EnableCors("MyCorsPolicy")]
        [Route("delete/{categoryId}")]
        public IActionResult DeleteCategory(string categoryId)
        {
            try
            {
                this.CategoryService.DeleteCategory(categoryId);

                return Ok("The category is successfully deleted");
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }
        }
    }
}
