namespace Data.Models.Abstractions
{
    using Data.Models.Interfaces;

    public abstract class BaseEntity : IEntity<string>
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAtUtc = DateTime.UtcNow;
            ModifiedAtUtc = DateTime.UtcNow;
            IsDeleted = false;
        }

        public string Id { get; init; }

        public DateTime CreatedAtUtc { get; init; }

        public DateTime ModifiedAtUtc { get; set; }

        public DateTime? DeletedAtUtc { get; init; }

        public bool IsDeleted { get; set; }
    }
}
