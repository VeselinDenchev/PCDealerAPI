namespace Data.Services.EntityServices
{
    using AutoMapper;

    using Data.DbContext;
    using Data.Services.EntityServices.Interfaces;

    using Data.Services.DtoModels;
    using Data.Models.Entities;

    public class SpecificationService : ISpecificationService
    {
        public SpecificationService(PcDealerDbContext dbContext, IMapper mapper, ISpecificationTypeService specificationTypeService)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
            this.SpecificationTypeService = specificationTypeService;
        }

        public PcDealerDbContext DbContext { get; init; }

        public IMapper Mapper { get; init; }

        public ISpecificationTypeService SpecificationTypeService { get; init; }

        /*public ICollection<SpecificationDto> GetAllProductSpecification(string productId)
        {
            Specification brand = this.DbContext.Specifications.Include(s => s.Type)
                                                        .AsNoTracking()
                                                        .FirstOrDefault(s => s.Id == brandId && s.IsDeleted == false);

            ICollection<SpecificationDto> brandSpecificationDtos = new List<SpecificationDto>();

            if (brand is not null)
            {
                var brandSpecifications = brand.Specifications.Where(m => m.IsDeleted == false).ToArray();
                brandSpecificationDtos = this.Mapper.Map<ICollection<Specification>, ICollection<SpecificationDto>>(brandSpecifications);
            }
            else throw new ArgumentException("Invalid brand!");

            return brandSpecificationDtos;
        }*/

        public void AddSpecification(string specificationTypeId, SpecificationDto specificationDto)
        {
            SpecificationTypeDto specificationType = this.SpecificationTypeService.GetSpecificationTypeByTypeId(specificationTypeId);
            if (specificationType is null) throw new ArgumentException("Invalid specification type!");

            specificationDto.Type = specificationType;

            Specification specification = this.Mapper.Map<SpecificationDto, Specification>(specificationDto);
            this.DbContext.Attach(specification.Type);
            this.DbContext.Specifications.Add(specification);

            this.DbContext.SaveChanges();
        }

        public void UpdateSpecification(SpecificationDto specificationDto, string specificationTypeId)
        {
            bool exists = this.DbContext.Specifications.Any(st => st.Id == specificationDto.Id);
            if (!exists) throw new ArgumentException("Such specification doesn't exist!");

            if (specificationTypeId is null)
            {
                specificationDto.Type = this.SpecificationTypeService.GetSpecificationTypeById(specificationDto.Id);
            }
            else
            {
                SpecificationTypeDto specificationTypeDto = 
                    this.SpecificationTypeService.GetSpecificationTypeByTypeId(specificationTypeId);
                if (specificationTypeDto is null) throw new ArgumentException("Such specification type doesn't exist!");

                specificationDto.Type = specificationTypeDto;
            }

            Specification specification = this.Mapper.Map<SpecificationDto, Specification>(specificationDto);
            this.DbContext.Attach(specification.Type);
            this.DbContext.Specifications.Update(specification);

            this.DbContext.SaveChanges();
        }

        public void DeleteSpecification(string specificationId)
        {
            bool exists = this.DbContext.Specifications.Any(st => st.Id == specificationId);
            if (!exists) throw new ArgumentException("Such specification doesn't exist!");

            Specification specificationToBeDeleted = this.DbContext.Specifications.Where(m => m.Id == specificationId).First();
            specificationToBeDeleted.IsDeleted = true;
            specificationToBeDeleted.DeletedAtUtc = DateTime.UtcNow;

            this.DbContext.SaveChanges();
        }
    }
}
