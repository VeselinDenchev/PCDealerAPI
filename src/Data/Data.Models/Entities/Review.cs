namespace Data.Models.Entities
{
    using Data.Models.Abstractions;
    using Data.Models.Abstractions.Interfaces;

    public class Review : BaseEntity, IReview
    {
        public User User { get; set; }

        public Product Product { get; set; }

        public float Rating { get; set; }
    }
}
