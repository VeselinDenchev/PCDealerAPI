namespace Data.Models.Entities
{
    using Data.Models.Abstractions;
    using Data.Models.Abstractions.Interfaces.Base;

    public class Model : BaseEntity, IName
    {
        public Brand Brand { get; set; }

        public string Name { get; set; }
    }
}
