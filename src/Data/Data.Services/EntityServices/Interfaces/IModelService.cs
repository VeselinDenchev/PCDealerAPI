namespace Data.Services.EntityServices.Interfaces
{
    using Data.Services.DtoModels;

    public interface IModelService
    {
        public ModelDto[] GetAllBrandModels(string brandId);

        public ModelDto GetModel(string brandId, string modelId);

        public void AddModel(string brandId, ModelDto modelDto);

        public void UpdateModel(string modelId, ModelDto modelDto);

        public void DeleteModel(string modelId);
    }
}
