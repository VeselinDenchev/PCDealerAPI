namespace Data.Services.DtoModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class CartItemDto : BaseDto
    {
        [JsonProperty(PropertyName = "productId")]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        [Range(0, short.MaxValue, ErrorMessage = "Quantity must be non negative!")]
        public short Quantity { get; set; }
    }
}
