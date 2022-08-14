namespace Data.Services.EntityServices.Interfaces
{
    using System.Collections.Generic;

    using Data.Services.DtoModels;

    public interface ISpecificationTypeService
    {
        public ICollection<SpecificationTypeDto> GetAllSpecificationTypes();

        public void AddSpecificationType(SpecificationTypeDto specificationTypeDto);

        public void UpdateSpecificationType(SpecificationTypeDto specificationTypeDto);

        public void DeleteSpecificationType(string specificationTypeId);
    }
}
