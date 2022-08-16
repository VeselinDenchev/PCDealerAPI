namespace Data.Services.EntityServices.Interfaces
{
    using Data.Services.DtoModels;

    public interface ICategoryService
    {
        public CategoryDto GetCategory(string categoryId);

        public void AddCategory(CategoryDto categoryDto);

        public void UpdateCategory(CategoryDto categoryDto);

        public void DeleteCategory(string categoryId);
    }
}
