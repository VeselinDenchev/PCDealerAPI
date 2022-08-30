namespace Data.Services.DtoModels
{
    using Constants;

    using Data.Models.Entities;

    using Newtonsoft.Json;

    public class ReviewDto : BaseDto
    {
        [JsonProperty(PropertyName = JsonConstant.USER_PROPERTY)]
        public User? User { get; set; }

        [JsonProperty(PropertyName = JsonConstant.PRODUCT_PROPERTY)]
        public ProductDto? Product { get; set; }

        [JsonProperty(PropertyName = JsonConstant.RATING_PROPERTY)]
        public float Rating { get; set; }

        [JsonProperty(PropertyName = JsonConstant.COMMENT_PROPERTY)]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = JsonConstant.MADE_ON_DATE_PROPERTY)]
        public string? MadeOnDate =>
            base.CreatedAtUtc.HasValue
            ? DateTime.Parse(base.CreatedAtUtc.ToString()).ToLocalTime().ToString(DateTimeFormat.CUSTOM_DATE_TIME_FORMAT)
            : null;
    }
}