namespace Data.Services.DtoModels
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class ProductDto : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public float Rating { get; set; }

        [JsonProperty(PropertyName = "brand")]
        public BrandDto Brand { get; set; }

        [JsonProperty(PropertyName = "model")]
        public ModelDto Model { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "images")]
        public ICollection<ImageDto> Images { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        [Range(0, short.MaxValue, ErrorMessage = "Quantity must be non negative!")]
        public short Quantity { get; set; }

        [JsonProperty(PropertyName = "specifications")]
        public ICollection<SpecificationDto> Specifications { get; set; }

        [JsonProperty(PropertyName = "category")]
        public CategoryDto Category { get; set; }
    }
}
