namespace Data.Services.EntityServices
{
    using AutoMapper;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        public ProductService(PcDealerDbContext dbContext, IMapper mapper, IModelService modelService, IImageService imageService)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
            this.ModelService = modelService;
            this.ImageService = imageService;
        }

        public PcDealerDbContext DbContext { get; init; }

        public IMapper Mapper { get; init; }

        public IModelService ModelService { get; init; }

        public IImageService ImageService { get; set; }

        public ProductDto[] GetAllProducts()
        {
            Product[] products = this.DbContext.Products
                                                .Where(p => p.IsDeleted == false && p.Model.IsDeleted == false)
                                                .Include(p => p.Model).ThenInclude(m => m.Brand)
                                                .Include(p => p.Model).ThenInclude(m => m.Category)
                                                .Include(p => p.Images.Where(i => i.IsDeleted == false))
                                                .ToArray();
            ProductDto[] productDtos = this.Mapper.Map<Product[], ProductDto[]>(products);

            foreach (ProductDto product in productDtos)
            {
                product.SalesCount = this.CalculateProductsSalesCount(product.Id);
            }

            return productDtos;
        }

        public ProductDto[] GetProductsByCategory(string categoryId)
        {
            Product[] products = this.DbContext.Products.Where(p => p.Model.Category.Id == categoryId && p.IsDeleted == false)
                                                            .Include(p => p.Model).ThenInclude(m => m.Category)
                                                            .ToArray();

            ProductDto[] producDtos = this.Mapper.Map<Product[], ProductDto[]>(products);

            return producDtos;
        }

        public ProductDto GetProduct(string productId)
        {
            Product product = this.DbContext.Products
                                            .Where(p => p.Id == productId && p.IsDeleted == false && p.Model.IsDeleted == false)
                                            .AsNoTracking()
                                            .Include(p => p.Model).ThenInclude(m => m.Brand)
                                            .Include(p => p.Model).ThenInclude(m => m.Category)
                                            .Include(p => p.Images.Where(i => i.IsDeleted == false))
                                            .FirstOrDefault();
            ProductDto productDto = this.Mapper.Map<Product, ProductDto>(product);

            return productDto;
        }

        public void AddProduct(ProductDto productDto, string modelId)
        {
            bool modelExists = this.DbContext.Models.Any(m => m.Id == modelId && m.IsDeleted == false);
            if (!modelExists) throw new ArgumentException("Such model doesn't exist!");

            productDto.Model = this.ModelService.GetModel(modelId);

            Product product = this.Mapper.Map<ProductDto, Product>(productDto);

            this.DbContext.Attach(product.Model);

            this.DbContext.Products.Add(product);
            this.DbContext.ImageFiles.AddRange(product.Images);

            this.DbContext.SaveChanges();

            productDto.CreatedAtUtc = product.CreatedAtUtc;
            productDto.ModifiedAtUtc = product.ModifiedAtUtc;

            productDto.SalesCount = 0;

            foreach (ImageDto image in productDto.Images)
            {
                image.CreatedAtUtc = product.CreatedAtUtc;
                image.ModifiedAtUtc = product.ModifiedAtUtc;
            }
        }

        public void UpdateProduct(ProductDto updatedProductDto, string modelId)
        {
            bool prodcutExists = this.DbContext.Products.Any(p => p.Id == updatedProductDto.Id);
            if (!prodcutExists) throw new ArgumentException("Such product doesn't exist!");


            if (modelId is null)
            {
                ModelDto modelDto = this.ModelService.GetModel(this.DbContext.Products.Where(p => p.Id == updatedProductDto.Id
                                                                                    && p.IsDeleted == false
                                                                                    && p.Model.IsDeleted == false)
                                                                        .Include(p => p.Model)
                                                                        .AsNoTracking()
                                                                        .First().Model.Id);

                updatedProductDto.Model = modelDto;
            }
            else
            {
                updatedProductDto.Model = this.ModelService.GetModel(this.DbContext.Models.Where(m => m.Id == modelId && m.IsDeleted == false)
                                                                                            .AsNoTracking()
                                                                                            .FirstOrDefault().Id);;

                if (updatedProductDto.Model is null) throw new ArgumentException("Such model doesn't exist!");
            }

            Product product = this.DbContext.Products.Where(p => p.Id == updatedProductDto.Id)
                                                        .AsNoTracking()
                                                        .Include(p => p.Images)
                                                        .First();
            DateTime productCreatedAtUtc = product.CreatedAtUtc;

            List<ImageFile> deletedImages = product.Images.Where(i => i.IsDeleted == false).ToList();

            product = this.Mapper.Map<ProductDto, Product>(updatedProductDto);
            product.CreatedAtUtc = productCreatedAtUtc;

            for (int i = 0; i < deletedImages.Count; i++)
            {
                foreach (var updatedImage in updatedProductDto.Images)
                {
                    bool isTheSameImage = deletedImages[i].Id == updatedImage.Id;
                    if (isTheSameImage)
                    {
                        deletedImages.Remove(deletedImages[i]);
                        i--;
                        break;
                    }
                }
            }

            List<ImageFile> newImages = new List<ImageFile>();
            List<ImageFile> updatedImages = new List<ImageFile>();

            ImageFile[] images = this.ImageService.GetAllImagesForProduct(product);

            foreach (ImageFile image in product.Images)
            {
                if (images.Any(i => i.FullFileName == image.FullFileName))
                {
                    updatedImages.Add(image);
                }
                else if (!deletedImages.Any(i => i.FullFileName == image.FullFileName))
                {
                    newImages.Add(image);
                }
            }

            foreach (var image in deletedImages)
            {
                image.IsDeleted = true;
                DeleteImage(image);
            }

            List<ImageFile> modifiedImages = updatedImages.Concat(deletedImages).ToList();

            this.DbContext.ImageFiles.AddRange(newImages);
            this.DbContext.ImageFiles.UpdateRange(modifiedImages);

            this.DbContext.Attach(product.Model);
            this.DbContext.Products.Update(product);

            this.DbContext.SaveChanges();

            updatedProductDto.CreatedAtUtc = productCreatedAtUtc;
            updatedProductDto.ModifiedAtUtc = product.ModifiedAtUtc;

            updatedProductDto.SalesCount = this.CalculateProductsSalesCount(updatedProductDto.Id);

            foreach (ImageDto image in updatedProductDto.Images)
            {
                if (newImages.Any(i => i.Id == image.Id))
                {
                    image.CreatedAtUtc = product.ModifiedAtUtc;
                    image.ModifiedAtUtc = product.ModifiedAtUtc;
                }
                else if (updatedImages.Any(i => i.Id == image.Id))
                {
                    image.CreatedAtUtc = productCreatedAtUtc;
                    image.ModifiedAtUtc = updatedImages.Where(i => i.Id == image.Id && i.IsDeleted == false).First().ModifiedAtUtc;
                }
            }
        }

        public void DeleteProduct(string productId)
        {
            bool exists = this.DbContext.Products.Any(b => b.Id == productId);
            if (!exists) throw new ArgumentException("Such product doesn't exist!");

            Product product = this.DbContext.Products.Where(r => r.Id == productId && r.IsDeleted == false)
                                                        .Include(r => r.Images.Where(i => i.IsDeleted == false))
                                                        .FirstOrDefault();
            ImageFile[] images = product.Images.ToArray();
            foreach (ImageFile image in images)
            {
                image.IsDeleted = true;
                image.DeletedAtUtc = DateTime.UtcNow;
                DeleteImage(image);
            }

            product.IsDeleted = true;
            product.DeletedAtUtc = DateTime.UtcNow;

            this.DbContext.Products.Update(product);
            this.DbContext.ImageFiles.UpdateRange(product.Images);
            this.DbContext.SaveChanges();
        }

        private void DeleteImage(ImageFile image)
        {
            try
            {
                File.Delete(@$"wwwroot\{image.Path}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while deleting {image.FullFileName}!");
            }
        }

        private int CalculateProductsSalesCount(string productId)
        {
            int productsSalesCount = 0;

            foreach (Order order in this.DbContext.Orders.Include(o => o.CartItems).ThenInclude(o => o.Product))
            {
                foreach (CartItem cartItem in order.CartItems)
                {
                    Product product = cartItem.Product;

                    bool wasInCart = product.Id == productId;
                    if (wasInCart)
                    {
                        productsSalesCount += product.Quantity;
                    }
                }
            }

            return productsSalesCount;
        }
    }
}
