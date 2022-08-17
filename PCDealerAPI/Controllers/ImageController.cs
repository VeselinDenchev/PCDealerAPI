namespace PCDealerAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
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
        [Route("/image/{imageId}")]
        public IActionResult GetImageById([FromRoute] string imageId)
        {
            byte[] imageFileByteArray = this.ImagesService.GetImageFileBytesArray(imageId, out string FullFileName);

            string fileMimeType = this.GetMimeType(FullFileName);

            if (imageFileByteArray is not null) return File(imageFileByteArray, fileMimeType);

            return NotFound("Invalid image id!");
        }

        [HttpGet]
        [Route("/image/all/{productId}")]
        public IActionResult GetAllProductImagesFullNames([FromRoute] string productId)
        {
            ProductDto product = this.ProductService.GetProduct(productId);

            if (product is not null)
            {
                string[] imagesFullNames = this.ImagesService.GetAllImagesFullNamesForProduct(product);

                return Ok(imagesFullNames);
            }

            return NotFound("Invalid product!");
        }

        // https://stackoverflow.com/questions/1029740/get-mime-type-from-filename-extension
        // Works only on Windows
        [NonAction]
        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}
