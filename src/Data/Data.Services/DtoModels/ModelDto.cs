namespace Data.Services.DtoModels
{
    using Constants;

    using Newtonsoft.Json;

    public class ModelDto : BaseDto
    {
        [JsonProperty(PropertyName = JsonConstant.NAME_PROPERTY)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = JsonConstant.BRAND_PROPERTY)]
        public BrandDto? Brand { get; set; }

        [JsonProperty(PropertyName = JsonConstant.CATEGORY_PROPERTY)]
        public CategoryDto? Category { get; set; }
    }
}
