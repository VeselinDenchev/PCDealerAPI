namespace Data.Services.DtoModels
{
    using Data.Services.EntityServices.Interfaces;

    using Newtonsoft.Json;

    public class BrandDto : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "productsCount")]
        public int? ProductsCount { get; set; }
    }
}
