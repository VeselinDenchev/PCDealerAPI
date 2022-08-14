namespace Data.Services.EntityServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;

    using Data.DbContext;
    using Data.Services.EntityServices.Interfaces;
    using Data.Services.DtoModels;
    using Data.Models.Entities;

    public class SpecificationTypeService : ISpecificationTypeService
    {
        public SpecificationTypeService(PcDealerDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public PcDealerDbContext DbContext { get; set; }

        public IMapper Mapper { get; set; }

        public ICollection<SpecificationTypeDto> GetAllSpecificationTypes()
        {
            ICollection<SpecificationType> specificationTypes = 
                this.DbContext.SpecificationTypes.Where(st => st.IsDeleted == false).ToArray();

            ICollection<SpecificationTypeDto> specificationTypesDtos = this.Mapper.Map<ICollection<SpecificationType>, ICollection<SpecificationTypeDto>>(specificationTypes);

            return specificationTypesDtos;
        }

        public void AddSpecificationType(SpecificationTypeDto specificationTypeDto)
        {
            SpecificationType specificationType = this.Mapper.Map<SpecificationTypeDto, SpecificationType>(specificationTypeDto);
            this.DbContext.SpecificationTypes.Add(specificationType);

            this.DbContext.SaveChanges();
        }

        public void UpdateSpecificationType(SpecificationTypeDto specificationTypeDto)
        {
            SpecificationType specificationType = this.Mapper.Map<SpecificationTypeDto, SpecificationType>(specificationTypeDto);
            this.DbContext.SpecificationTypes.Update(specificationType);

            this.DbContext.SaveChanges();
        }

        public void DeleteSpecificationType(string specificationTypeId)
        {
            SpecificationType specificationTypeToBeDeleted = this.DbContext.SpecificationTypes.Where(m => m.Id == specificationTypeId).FirstOrDefault();
            specificationTypeToBeDeleted.IsDeleted = true;
            specificationTypeToBeDeleted.DeletedAtUtc = DateTime.UtcNow;

            this.DbContext.SaveChanges();
        }
    }
}
