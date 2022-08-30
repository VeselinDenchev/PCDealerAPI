namespace Data.Models.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Constants;

    using Data.Models.Abstractions;
    using Data.Models.Abstractions.Interfaces;

    public class CartItem : BaseEntity, ICartItem
    {
        public Product Product { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = ErrorMessage.NEGATIVE_QUANTITY_MESSAGE)]
        public short Quantity { get; set; }
    }
}
