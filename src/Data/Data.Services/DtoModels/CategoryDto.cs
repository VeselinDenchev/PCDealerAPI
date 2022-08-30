namespace Data.Services.DtoModels
{
    using Constants;

    using Newtonsoft.Json;

    public class CategoryDto : BaseDto
    {
        [JsonProperty(JsonConstant.NAME_PROPERTY)]
        public string Name { get; set; }
    }
}
