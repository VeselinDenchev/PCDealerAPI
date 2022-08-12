namespace Data.Models.Abstractions.Interfaces
{
    using Data.Models.Entities;

    public interface IOrder
    {
        public ICollection<CartItem> CartItems { get; set; }
    }
}
