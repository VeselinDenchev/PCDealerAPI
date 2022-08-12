namespace Data.Models.Abstractions.Interfaces
{
    using Data.Models.Abstractions.Interfaces.Base;
    using Data.Models.Entities;

    public interface IBrand : IName
    {
        public ICollection<Model> Models { get; set; }
    }
}
