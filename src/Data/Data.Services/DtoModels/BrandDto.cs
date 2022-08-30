namespace Data.Services.DtoModels
{
    using Constants;

    using Data.Services.EntityServices.Interfaces;

    using Newtonsoft.Json;

    public class BrandDto : BaseDto
    {
        [JsonProperty(JsonConstant.NAME_PROPERTY)]
        public string Name { get; set; }

        [JsonProperty(JsonConstant.PRODUCTS_COUNT_PROPERTY)]
        public int? ProductsCount { get; set; }
    }
}
