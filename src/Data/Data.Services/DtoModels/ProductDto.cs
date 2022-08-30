namespace Data.Services.DtoModels
{
    using System.ComponentModel.DataAnnotations;

    using Constants;

    using Newtonsoft.Json;

    public class ProductDto : BaseDto
    {
        [JsonProperty(PropertyName = JsonConstant.NAME_PROPERTY)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = JsonConstant.PROCESSOR_PROPERTY)]
        public string Processor { get; set; }

        [JsonProperty(PropertyName = JsonConstant.RAM_PROPERTY)]
        public string Ram { get; set; }

        [JsonProperty(PropertyName = JsonConstant.GPU_PROPERTY)]
        public string GPU { get; set; }

        [JsonProperty(PropertyName = JsonConstant.STORAGE_PROPERTY)]
        public string Storage { get; set; }

        [JsonProperty(PropertyName = JsonConstant.DISPLAY_PROPERTY)]
        public string Display { get; set; }

        [JsonProperty(PropertyName = JsonConstant.RATING_PROPERTY)]
        public float? Rating { get; set; }

        [JsonProperty(PropertyName = JsonConstant.MODEL_PROPERTY)]
        public ModelDto? Model { get; set; }

        [JsonProperty(PropertyName = JsonConstant.PRICE_PROPERTY)]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = JsonConstant.IMAGES_PROPERTY)]
        public ICollection<ImageDto>? Images { get; set; }

        [JsonProperty(PropertyName = JsonConstant.DESCRIPTION_PROPERTY)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = JsonConstant.QUANTITY_PROPERTY)]
        [Range(0, short.MaxValue, ErrorMessage = ErrorMessage.NEGATIVE_QUANTITY_MESSAGE)]
        public short Quantity { get; set; }

        [JsonProperty(PropertyName = JsonConstant.SALES_COUNT_PROPERTY)]
        public int? SalesCount { get; set; }
    }
}
