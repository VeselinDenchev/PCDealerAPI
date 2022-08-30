namespace Data.Services.DtoModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Constants;

    using Newtonsoft.Json;

    public class CartItemDto : BaseDto
    {
        [JsonProperty(PropertyName = JsonConstant.PRODUCT_ID_PROPERTY)]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = JsonConstant.QUANTITY_PROPERTY)]
        [Range(0, short.MaxValue, ErrorMessage = ErrorMessage.NEGATIVE_QUANTITY_MESSAGE)]
        public short Quantity { get; set; }
    }
}
