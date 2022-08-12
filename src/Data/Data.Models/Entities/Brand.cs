namespace Data.Models.Entities
{
    using Data.Models.Abstractions;
    using Data.Models.Abstractions.Interfaces;

    public class Brand : BaseEntity, IBrand
    {
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }
    }
}
