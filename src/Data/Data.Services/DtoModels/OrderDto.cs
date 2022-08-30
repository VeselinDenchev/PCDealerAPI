namespace Data.Services.DtoModels
{
    using Constants;

    using Data.Models.Entities;

    using Newtonsoft.Json;

    public class OrderDto : BaseDto
    {
        [JsonProperty(PropertyName = JsonConstant.CART_ITEMS_PROPERTY)]
        public ICollection<CartItemDto> CartItems { get; set; }

        [JsonProperty(PropertyName = JsonConstant.USER_PROPERTY)]
        public User? User { get; set; }

        [JsonProperty(PropertyName = JsonConstant.FIRST_NAME_PROPERTY)]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = JsonConstant.LAST_NAME_PROPERTY)]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = JsonConstant.PHONE_NUMBER_PROPERTY)]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = JsonConstant.ADDRESS_PROPERTY)]
        public string Address { get; set; }

        [JsonProperty(PropertyName = JsonConstant.SUB_TOTAL_PROPERTY)]
        public decimal SubTotal { get; set; }

        [JsonProperty(PropertyName = JsonConstant.SHIPPING_COST_PROPERTY)]
        public decimal ShippingCost => this.SubTotal < 1_000 ? 5 : 0;

        [JsonProperty(PropertyName = JsonConstant.GRAND_TOTAL_PROPERTY)]
        public decimal GrandTotal => this.SubTotal + this.ShippingCost;

        [JsonProperty(PropertyName = JsonConstant.CREATED_AT_STRING)]
        public string? CreatedAtString => 
            base.CreatedAtUtc.HasValue 
            ? DateTime.Parse(base.CreatedAtUtc.ToString()).ToLocalTime().ToString(DateTimeFormat.CUSTOM_DATE_TIME_FORMAT) 
            : null;

        public string? Status
        {
            get
            {
                if (base.CreatedAtUtc.HasValue)
                {
                    return DateTime.UtcNow > ((DateTime)base.CreatedAtUtc).AddMinutes(OrderConstant.DELIVERY_MINUTES) 
                                             ? OrderConstant.ORDER_STATUS_COMPLETED 
                                             : OrderConstant.ORDER_STATUS_PENDING;
                }

                return null;
            }
        }
    }
}
