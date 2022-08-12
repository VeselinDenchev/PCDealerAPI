namespace Data.Models.Entities
{
    using Data.Models.Abstractions;
    using Data.Models.Abstractions.Interfaces;

    public class Specification : BaseEntity, ISpecification
    {
        public SpecificationType Type { get; set; }

        public string Value { get; set; }
    }
}
