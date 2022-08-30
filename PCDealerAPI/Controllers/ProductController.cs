namespace PCDealerAPI.Controllers
{
    using Constants;

    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route(ControllerConstant.CONTROLLER_BASE_ROUTE)]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController(IWebHostEnvironment webHostEnvironment, IProductService productsService, IImageService imageService)
        {
            this.WebHostEnvironment = webHostEnvironment;
            this.ProductsService = productsService;
            this.ImagesService = imageService;
        }

        public IWebHostEnvironment WebHostEnvironment { get; init; }

        public IProductService ProductsService { get; init; }

        public IImageService ImagesService { get; init; }

        [HttpGet]
        [Route(ControllerConstant.ALL_ROUTE)]
        public IActionResult GetAllProducts()
        {
            ProductDto[] products = this.ProductsService.GetAllProducts();

            return Ok(products);
        }

        [HttpGet]
        [Route(ControllerConstant.GET_PRODUCTS_BY_CATEGORY_ROUTE)]
        public IActionResult GetProductsByCategory([FromRoute] string categoryId)
        {
            ProductDto[] products = this.ProductsService.GetProductsByCategory(categoryId);

            return Ok(products);
        }

        [HttpGet]
        [Route(ControllerConstant.PRODUCT_ID_PARAMETER)]
        public IActionResult GetProduct([FromRoute] string productId)
        {
            ProductDto product = this.ProductsService.GetProduct(productId);

            return Ok(product);
        }

        [HttpPost]
        [Consumes(ControllerConstant.MULTIPART_FORM_DATA_REQUEST_TYPE)]
        [Route(ControllerConstant.ADD_PRODUCT_ROUTE)]
        public IActionResult AddProduct([FromRoute] string modelId, [FromForm] ProductDto product, 
                                        [FromForm] IFormFileCollection files)
        {
            ObjectResult result = ValidateFiles(product, files.ToList());

            if (result.GetType() == typeof(OkObjectResult))
            {
                try
                {
                    this.ProductsService.AddProduct(product, modelId);
                }
                catch (ArgumentException ae)
                {
                    result = NotFound(ae.Message);
                }
            }

            return result;
        }

        [HttpPut]
        [Consumes(ControllerConstant.MULTIPART_FORM_DATA_REQUEST_TYPE)]
        [Route(
            $"{ControllerConstant.UPDATE_ROUTE}" +
            $"{ControllerConstant.PRODUCT_ID_PARAMETER}/" +
            $"{ControllerConstant.MODEL_ID_OPTIONAL_PARAMETER}"
        )]
        public IActionResult UpdateProduct([FromRoute] string productId, [FromRoute] string? modelId, [FromForm] ProductDto product, 
                                            [FromForm] IFormFileCollection files)
        {
            product.Id = productId;

            ObjectResult result = ValidateFiles(product, files.ToList());

            if (result.GetType() == typeof(OkObjectResult))
            {
                this.ProductsService.UpdateProduct(product, modelId);
            }

            return result;
        }

        [HttpDelete]
        [Route(ControllerConstant.DELETE_ROUTE + ControllerConstant.PRODUCT_ID_PARAMETER)]
        public IActionResult DeleteProduct([FromRoute] string productId)
        {
            if (productId is null)
            {
                return BadRequest(ErrorMessage.NON_EXISTING_PRODUCT_MESSAGE);
            }

            try
            {
                this.ProductsService.DeleteProduct(productId);
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }

            return Ok(InfoMessage.PRODUCT_SUCCESSFULLY_DELETED_MESSAGE);
        }

        [NonAction]
        private ImageDto[] ConvertFormFilesToImages(List<IFormFile> files)
        {
            ImageDto[] imagesDtos = new ImageDto[files.Count];

            if (files is not null && files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    FileInfo fileInfo = new FileInfo(files[i].FileName);
                    string newFileName = this.ImagesService.GenerateUniqueFileName();
                    string fullFileName = newFileName + fileInfo.Extension;
                    string path = Path.Combine("", WebHostEnvironment.WebRootPath + "/images/" + fullFileName);

                    using (FileStream fileStream = new FileStream(path, FileMode.Create))
                    {
                        files[i].CopyTo(fileStream);
                    }

                    ImageDto image = new ImageDto(newFileName, fileInfo.Extension);
                    imagesDtos[i] = image;
                }
            }
            else throw new FileNotFoundException(ErrorMessage.IMAGE_REQUIRED_MESSAGE);

            return imagesDtos;
        }

        [NonAction]
        private ObjectResult ValidateFiles(ProductDto product, List<IFormFile> files)
        {
            if (product is null) return BadRequest(ErrorMessage.NON_EXISTING_PRODUCT_MESSAGE);

            List<ImageDto> imageDtos = new List<ImageDto>();

            var filesNames = files.Select(f => f.FileName).ToList();

            var imagesFullNames = this.ImagesService.GetAllImagesFullNamesForProduct(product);

            if (imagesFullNames is not null)
            {
                foreach (var imageFullName in imagesFullNames)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        bool isTheSameImage = files[i].FileName == imageFullName;
                        if (isTheSameImage)
                        {
                            imageDtos.Add(this.ImagesService.GetImageByFullFilleName(imageFullName));
                            files.Remove(files[i]);
                        }

                        if (files.Count == 0) break;
                    }
                }
            }

            try
            {
                if (files.Count > 0)
                {
                    imageDtos.AddRange(ConvertFormFilesToImages(files));
                }
            }
            catch (FileNotFoundException fnfe)
            {
                return NotFound(fnfe.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            product.Images = imageDtos.ToArray();

            return Ok(product);
        }
    }
}