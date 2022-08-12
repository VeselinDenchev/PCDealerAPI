namespace Data.Models.Entities
{
    using System.Collections.Generic;

    using Data.Models.Abstractions;
    using Data.Models.Abstractions.Interfaces;

    public class Order : BaseEntity, IOrder
    {
        public ICollection<CartItem> CartItems { get; set; }
    }
}
