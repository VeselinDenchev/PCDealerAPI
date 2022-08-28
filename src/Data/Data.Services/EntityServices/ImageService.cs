namespace Data.Services.EntityServices
{
    using System.Collections.Generic;

    using AutoMapper;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ImageService : IImageService
    {
        public ImageService(PcDealerDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public PcDealerDbContext DbContext { get; set; }

        public IMapper Mapper { get; set; }

        public ImageFile[] Add(ICollection<ImageDto> imageDtos)
        {
            ICollection<ImageFile> images = this.Mapper.Map<ICollection<ImageDto>, ICollection<ImageFile>>(imageDtos);

            try
            {
                this.DbContext.ImageFiles.AddRange(images);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return images.ToArray();
        }

        public string GenerateUniqueFileName() => Guid.NewGuid().ToString().Substring(0, 10);
        public string[] GetAllImagesFullNamesForProduct(ProductDto product)
        {
            ImageDto[] images = this.GetAllImagesForProduct(product);

            if (images is null) return null;
            return images.Select(r => r.FullFileName).ToArray();
        }

        public ImageFile[] GetAllImages() => this.DbContext.ImageFiles.Where(i => i.IsDeleted == false).AsNoTracking().ToArray();

        public ImageFile[] GetAllImagesForProduct(Product product) => this.DbContext.Products
                                                                        .Where(p => p.Id == product.Id)
                                                                        .Include(p => p.Images.Where(i => i.IsDeleted == false))
                                                                        .AsNoTracking()
                                                                        .FirstOrDefault().Images
                                                                        .ToArray();

        public ImageDto GetImageByFullFilleName(string fullFileName)
        {
            ImageFile image = this.DbContext.ImageFiles
                            .Where(i => (i.FileName + i.FileExtension) == fullFileName && i.IsDeleted == false)
                            .AsNoTracking()
                            .FirstOrDefault();
            ImageDto imageDto = this.Mapper.Map<ImageFile, ImageDto>(image);

            return imageDto;
        }

        public byte[]? GetImageFileBytesArray(string imageId, out string imageFullFileName)
        {
            ImageFile image = this.DbContext.ImageFiles.Where(i => i.Id == imageId && i.IsDeleted == false).FirstOrDefault();

            imageFullFileName = image.FullFileName;
            string imagePath = image.Path;

            if (imagePath is not null)
            {
                byte[] imageBytesArray = File.ReadAllBytes(@$"wwwroot/{imagePath}");
                return imageBytesArray;
            }

            return null;
        }

        private ImageDto[] GetAllImagesForProduct(ProductDto productDto)
        {
            Product product = this.DbContext.Products.Where(p => p.Id == productDto.Id)
                                                        .Include(r => r.Images)
                                                        .AsNoTracking()
                                                        .FirstOrDefault();

            if (product is null) return null;

            ImageDto[] imagesDtos = this.Mapper.Map<ImageFile[], ImageDto[]>(product.Images.ToArray());

            return imagesDtos;
        }
    }
}