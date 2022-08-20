namespace Data.Services.DtoModels
{
    using Data.Models.Entities;

    using Newtonsoft.Json;

    public class OrderDto : BaseDto
    {
        [JsonProperty(PropertyName = "cartItems")]
        public ICollection<CartItemDto> CartItems { get; set; }

        [JsonProperty(PropertyName = "user")]
        public User? User { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "subTotal")]
        public decimal SubTotal { get; set; }

        [JsonProperty(PropertyName = "shippingCost")]
        public decimal ShippingCost => this.SubTotal < 1_000 ? 5 : 0;

        [JsonProperty(PropertyName = "grandTotal")]
        public decimal GrandTotal => this.SubTotal + this.ShippingCost;

        [JsonProperty(PropertyName = "createdAtString")]
        public string? CreatedAtString => 
            base.CreatedAtUtc.HasValue 
            ? DateTime.Parse(base.CreatedAtUtc.ToString()).ToLocalTime().ToString("dd/MM/yyyy HH:mm") 
            : null;

        public string? Status
        {
            get
            {
                if (base.CreatedAtUtc.HasValue)
                {
                    return DateTime.UtcNow > ((DateTime)base.CreatedAtUtc).AddMinutes(1) ? "Completed" : "Pending";
                }

                return null;
            }
        }
    }
}
