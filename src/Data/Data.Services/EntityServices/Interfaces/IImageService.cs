namespace Data.Services.EntityServices.Interfaces
{
    using Data.Models.Entities;
    using Data.Services.DtoModels;

    public interface IImageService
    {
        public ImageFile[] GetAllImages();

        public ImageFile[] GetAllImagesForProduct(Product product);

        public string[] GetAllImagesFullNamesForProduct(ProductDto product);

        public ImageFile[] Add(ICollection<ImageDto> imageDtos);

        public string GenerateUniqueFileName();

        public byte[]? GetImageFileBytesArray(string imageId, out string imageFileName);

        public ImageDto GetImageByFullFilleName(string fullFileName);
    }
}
