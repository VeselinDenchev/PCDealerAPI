namespace PCDealerAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
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
        [Route("all")]
        public IActionResult GetAllProducts()
        {
            ProductDto[] products = this.ProductsService.GetAllProducts();

            return Ok(products);
        }


        [HttpGet]
        [EnableCors("MyCorsPolicy")]
        [Route("{productId}")]
        public IActionResult GetProduct([FromRoute] string productId)
        {
            ProductDto product = this.ProductsService.GetProduct(productId);

            return Ok(product);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Route("model/{modelId}/add")]
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
        [Consumes("multipart/form-data")]
        [EnableCors("MyCorsPolicy")]
        [Route("update/{productId}/{modelId?}")]
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
        [Route("delete/{productId}")]
        public IActionResult DeleteProduct([FromRoute] string productId)
        {
            if (productId is null)
            {
                return BadRequest("Invalid product id!");
            }

            try
            {
                this.ProductsService.DeleteProduct(productId);
            }
            catch (ArgumentException ae)
            {
                return NotFound(ae.Message);
            }

            return Ok("Product deleted sucessfully!");
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
            else throw new FileNotFoundException("You must upload images!");

            return imagesDtos;
        }

        [NonAction]
        private ObjectResult ValidateFiles(ProductDto product, List<IFormFile> files)
        {
            if (product is null) return BadRequest("Invalid product!");

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

            product.Images = imageDtos;

            return Ok(product);
        }
    }
}
