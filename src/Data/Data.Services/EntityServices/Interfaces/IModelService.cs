namespace Data.Services.EntityServices.Interfaces
{
    using Data.Services.DtoModels;

    public interface IModelService
    {
        public ICollection<ModelDto> GetAllBrandModels(string brandId);

        public ModelDto GetModel(string modelId);

        public void AddModel(string brandId, ModelDto modelDto);

        public void UpdateModel(ModelDto modelDto);

        public void DeleteModel(string modelId);
    }
}
