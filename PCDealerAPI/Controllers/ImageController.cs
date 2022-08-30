namespace PCDealerAPI.Controllers
{
    using Constants;

    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    [Route(ControllerConstant.CONTROLLER_BASE_ROUTE)]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public ImageController(IImageService imagesService, IProductService productService)
        {
            this.ImagesService = imagesService;
            this.ProductService = productService;
        }

        public IImageService ImagesService { get; }

        public IProductService ProductService { get; init; }

        [HttpGet]
        [Route(ControllerConstant.IMAGE_ID_PARAMETER)]
        public IActionResult GetImage([FromRoute] string imageId)
        {
            byte[] imageFileByteArray = this.ImagesService.GetImageFileBytesArray(imageId, out string FullFileName);

            string fileMimeType = this.ImagesService.GetMimeType(FullFileName);

            if (imageFileByteArray is not null) return File(imageFileByteArray, fileMimeType);

            return NotFound(ErrorMessage.INVALID_IMAGE_ID_MESSAGE);
        }

        [HttpGet]
        [Route($"{ControllerConstant.ALL_ROUTE}/{ControllerConstant.PRODUCT_ID_PARAMETER}")]
        public IActionResult GetAllProductImagesFullNames([FromRoute] string productId)
        {
            ProductDto product = this.ProductService.GetProduct(productId);

            if (product is not null)
            {
                string[] imagesFullNames = this.ImagesService.GetAllImagesFullNamesForProduct(product);

                return Ok(imagesFullNames);
            }

            return NotFound(ErrorMessage.NON_EXISTING_PRODUCT_MESSAGE);
        }
    }
}
