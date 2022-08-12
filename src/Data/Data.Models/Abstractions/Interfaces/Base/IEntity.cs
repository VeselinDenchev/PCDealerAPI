namespace Data.Models.Abstractions.Interfaces.Base
{
    public interface IEntity<T> : IIdentity<T>, ICreatedInfo, IModifiedInfo, IDeletedInfo
    {
    }
}
