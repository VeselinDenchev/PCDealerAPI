namespace Data.Services.EntityServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Data.DbContext;
    using Data.Services.EntityServices.Interfaces;
    using Data.Services.DtoModels;
    using Data.Models.Entities;
    using Microsoft.EntityFrameworkCore;

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

        public SpecificationTypeDto GetSpecificationTypeByTypeId(string specificationTypeId)
        {
            SpecificationType specificationType = this.DbContext.SpecificationTypes
                                                                    .Where(st => st.Id == specificationTypeId && 
                                                                            st.IsDeleted == false)
                                                                    .AsNoTracking()
                                                                    .FirstOrDefault();
            SpecificationTypeDto specificationTypeDto = this.Mapper.Map<SpecificationType, SpecificationTypeDto>(specificationType);

            return specificationTypeDto;
        }

        public SpecificationTypeDto GetSpecificationTypeById(string specificationId)
        {
            SpecificationType specificationType = this.DbContext.Specifications.Where(s => s.Id == specificationId)
                                                                    .Include(s => s.Type)
                                                                    .AsNoTracking()
                                                                    .First().Type;
            SpecificationTypeDto specificationTypeDto = this.Mapper.Map<SpecificationType, SpecificationTypeDto>(specificationType);

            return specificationTypeDto;
        }

        public void AddSpecificationType(SpecificationTypeDto specificationTypeDto)
        {
            SpecificationType specificationType = this.Mapper.Map<SpecificationTypeDto, SpecificationType>(specificationTypeDto);
            this.DbContext.SpecificationTypes.Add(specificationType);

            this.DbContext.SaveChanges();
        }

        public void UpdateSpecificationType(SpecificationTypeDto specificationTypeDto)
        {
            bool exists = this.DbContext.SpecificationTypes.Any(st => st.Id == specificationTypeDto.Id);
            if (!exists) throw new ArgumentException("Such specification type doesn't exist!");

            SpecificationType specificationType = this.Mapper.Map<SpecificationTypeDto, SpecificationType>(specificationTypeDto);
            this.DbContext.SpecificationTypes.Update(specificationType);

            this.DbContext.SaveChanges();
        }

        public void DeleteSpecificationType(string specificationTypeId)
        {
            bool exists = this.DbContext.SpecificationTypes.Any(st => st.Id == specificationTypeId);
            if (!exists) throw new ArgumentException("Such specification type doesn't exist!");

            SpecificationType specificationTypeToBeDeleted = this.DbContext.SpecificationTypes.Where(m => m.Id == specificationTypeId)
                                                                                                .First();
            specificationTypeToBeDeleted.IsDeleted = true;
            specificationTypeToBeDeleted.DeletedAtUtc = DateTime.UtcNow;

            this.DbContext.SaveChanges();
        }
    }
}
