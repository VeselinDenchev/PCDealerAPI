namespace Data.Models.Abstractions.Interfaces.Base
{
    public interface IIdentity<T>
    {
        public T Id { get; init; }
    }
}
