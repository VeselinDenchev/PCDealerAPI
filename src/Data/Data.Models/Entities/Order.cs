namespace Data.Models.Entities
{
    using System.Collections.Generic;

    using Constants;

    using Data.Models.Abstractions;
    using Data.Models.Abstractions.Interfaces;

    public class Order : BaseEntity, IOrder
    {
        public ICollection<CartItem> CartItems { get; set; }

        public User User { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }

        public decimal? SubTotal { get; set; }

        public string Status => DateTime.UtcNow > base.CreatedAtUtc.AddMinutes(OrderConstant.DELIVERY_MINUTES) 
                                ? OrderConstant.ORDER_STATUS_COMPLETED 
                                : OrderConstant.ORDER_STATUS_PENDING;
    }
}
