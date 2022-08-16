namespace Data.Services.EntityServices.Interfaces
{
    using Data.Services.DtoModels;

    public interface IProductService
    {
        public ProductDto[] GetAllProducts();

        public ProductDto GetProduct(string productId);

        public void AddProduct(ProductDto productDto, string modelId);

        public void UpdateProduct(ProductDto updatedProductDto, string modelId);

        public void DeleteProduct(string productId);
    }
}
