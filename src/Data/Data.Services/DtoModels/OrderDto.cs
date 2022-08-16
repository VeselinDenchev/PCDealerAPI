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

        public string? Status
        {
            get
            {
                if (base.CreatedAtUtc.HasValue)
                {
                    return DateTime.UtcNow > ((DateTime)base.CreatedAtUtc).AddDays(3) ? "Completed" : "Pending";
                }

                return null;
            }
        }
    }
}
