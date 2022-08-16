namespace Data.Services.DtoModels
{
    using Newtonsoft.Json;

    public class ModelDto : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "brand")]
        public BrandDto? Brand { get; set; }

        [JsonProperty(PropertyName = "category")]
        public CategoryDto? Category { get; set; }
    }
}
