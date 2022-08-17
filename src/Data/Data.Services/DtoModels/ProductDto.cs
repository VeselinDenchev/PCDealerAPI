namespace Data.Services.DtoModels
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class ProductDto : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "processor")]
        public string Processor { get; set; }

        [JsonProperty(PropertyName = "ram")]
        public string Ram { get; set; }

        [JsonProperty(PropertyName = "gpu")]
        public string GPU { get; set; }

        [JsonProperty(PropertyName = "storage")]
        public string Storage { get; set; }

        [JsonProperty(PropertyName = "display")]
        public string Display { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public float? Rating { get; set; }

        [JsonProperty(PropertyName = "model")]
        public ModelDto? Model { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "images")]
        public ICollection<ImageDto>? Images { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        [Range(0, short.MaxValue, ErrorMessage = "Quantity must be non negative!")]
        public short Quantity { get; set; }

        [JsonProperty(PropertyName = "salesCount")]
        public int? SalesCount { get; set; }
    }
}
