namespace Data.Services.DtoModels
{
    using Newtonsoft.Json;

    public class OrderDto : BaseDto
    {
        [JsonProperty(PropertyName = "cartItems")]
        public ICollection<CartItemDto> CartItems { get; set; }

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
