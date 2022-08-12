namespace Data.Models.Entities
{
    using Data.Models.Abstractions;
    using Data.Models.Abstractions.Interfaces;

    public class Review : BaseEntity, IReview
    {
        public IUser User { get; set; }

        public IProduct Product { get; set; }

        public float Rating { get; set; }
    }
}
