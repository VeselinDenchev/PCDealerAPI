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

        [JsonProperty(PropertyName = "createdAtUtc")]
        public DateTime? CreatedAtUtc { get; set; }

        [JsonProperty(PropertyName = "modifiedAtUtc")]
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
