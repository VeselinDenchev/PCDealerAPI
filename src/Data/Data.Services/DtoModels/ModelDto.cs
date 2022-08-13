namespace Data.Services.DtoModels
{
    using Newtonsoft.Json;

    public class ModelDto : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
