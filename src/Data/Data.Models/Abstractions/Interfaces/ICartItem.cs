namespace Data.Models.Abstractions.Interfaces
{
    using Data.Models.Entities;


    public interface ICartItem : IQuantity
    {
        public Product Product { get; set; }
    }
}
