namespace Data.Models.Abstractions.Interfaces.Base
{
    public interface IDeletedInfo
    {
        public DateTime? DeletedAtUtc { get; set; }

        public bool IsDeleted { get; set; }
    }
}
