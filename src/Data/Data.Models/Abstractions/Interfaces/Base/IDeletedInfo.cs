namespace Data.Models.Abstractions.Interfaces.Base
{
    public interface IDeletedInfo
    {
        public DateTime? DeletedAtUtc { get; init; }

        public bool IsDeleted { get; set; }
    }
}
