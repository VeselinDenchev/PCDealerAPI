namespace Data.Services.EntityServices.Interfaces
{
    using Data.Services.DtoModels;

    public interface ISpecificationService
    {
        public void AddSpecification(string specificationTypeId, SpecificationDto specificationDto);

        public void UpdateSpecification(SpecificationDto specificationDto, string specificationTypeId);

        public void DeleteSpecification(string specificationId);
    }
}
