namespace Data.Models.Abstractions.Interfaces
{
    using Data.Models.Abstractions.Interfaces.Base;

    public interface IReview : IRating
    {
        public IUser User { get; set; }

        public IProduct Product { get; set; }
    }
}
