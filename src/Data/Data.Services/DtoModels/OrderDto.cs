namespace Data.Services.DtoModels
{
    using Newtonsoft.Json;

    public class OrderDto : BaseDto
    {
        [JsonProperty(PropertyName = "cartItems")]
        public ICollection<CartItemDto> CartItems { get; set; }
    }
}
