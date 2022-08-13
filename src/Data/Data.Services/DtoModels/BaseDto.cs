namespace Data.Services.DtoModels
{
    using Newtonsoft.Json;

    public class BaseDto
    {
        public BaseDto()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
    }
}
