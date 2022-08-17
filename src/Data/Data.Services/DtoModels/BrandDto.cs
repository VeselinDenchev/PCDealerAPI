namespace Data.Services.DtoModels
{
    using Data.Services.EntityServices.Interfaces;

    using Newtonsoft.Json;

    public class BrandDto : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "brandProductsCount")]
        public int? BrandProductsCount { get; set; }
    }
}
