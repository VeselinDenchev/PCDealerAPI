namespace Data.Models.Abstractions
{
    using Data.Models.Abstractions.Interfaces.Base;

    public abstract class BaseEntity : IEntity<string>
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAtUtc = DateTime.UtcNow;
            this.ModifiedAtUtc = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        public string Id { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime ModifiedAtUtc { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public bool IsDeleted { get; set; }
    }
}
