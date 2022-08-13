namespace Data.Services.DtoModels
{
    using Newtonsoft.Json;

    public class CategoryDto : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
