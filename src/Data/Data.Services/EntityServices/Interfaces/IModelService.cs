namespace Data.Services.EntityServices.Interfaces
{
    using Data.Services.DtoModels;

    public interface IModelService
    {
        public ICollection<ModelDto> GetAllBrandModels(string brandId);

        public ModelDto GetModel(string modelId);

        public void AddModel(ModelDto modelDto, string brandId, string categoryId);

        public void UpdateModel(ModelDto modelDto, string categoryId);

        public void DeleteModel(string modelId);
    }
}
