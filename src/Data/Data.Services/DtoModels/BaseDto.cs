namespace Data.Services.DtoModels
{
    using Constants;

    using Newtonsoft.Json;

    public class BaseDto
    {
        public BaseDto()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [JsonProperty(PropertyName = JsonConstant.ID_PROPERTY)]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = JsonConstant.CREATED_AT_UTC_PROPERTY)]
        public DateTime? CreatedAtUtc { get; set; }

        [JsonProperty(PropertyName = JsonConstant.MODIFIED_AT_UTC_PROPERTY)]
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
