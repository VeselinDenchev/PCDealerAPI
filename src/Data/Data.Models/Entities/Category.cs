namespace Data.Models.Entities
{
    using Data.Models.Abstractions;
    using Data.Models.Abstractions.Interfaces.Base;

    public class Category : BaseEntity, IName
    {
        public string Name { get; set; }
    }
}
