namespace Data.Services.DtoModels
{
    using Newtonsoft.Json;

    public class SpecificationTypeDto : BaseDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
