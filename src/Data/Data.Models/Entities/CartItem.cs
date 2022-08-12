namespace Data.Models.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models.Abstractions;
    using Data.Models.Abstractions.Interfaces;

    public class CartItem : BaseEntity, ICartItem
    {
        public IProduct Product { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Quantity must be non negative!")]
        public short Quantity { get; set; }
    }
}
