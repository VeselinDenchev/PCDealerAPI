namespace Data.Models.Abstractions.Interfaces
{
    using Data.Models.Entities;

    public interface ISpecification
    {
        public SpecificationType Type { get; set; }

        public string Value { get; set; }
    }
}
