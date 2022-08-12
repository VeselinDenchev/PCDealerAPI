namespace Data.Models.Abstractions.Interfaces
{
    using Data.Models.Abstractions.Interfaces.Base;
    using Data.Models.Entities;

    public interface IReview : IRating
    {
        public User User { get; set; }

        public Product Product { get; set; }
    }
}
