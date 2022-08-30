namespace Data.Services.DtoModels.Jwt
{
    using Constants;

    using Newtonsoft.Json;

    [JsonObject(JsonConstant.JWT_OBJECT)]
    public class TokenModel
    {
        [JsonProperty(JsonConstant.TOKEN_SECRET_PROPERTY)]
        public string TokenSecret { get; set; }

        [JsonProperty(JsonConstant.VALIDATE_ISSUER_PROPERTY)]
        public string ValidateIssuer { get; set; }

        [JsonProperty(JsonConstant.VALIDATE_AUDIENCE_PROPERTY)]
        public string ValidateAudience { get; set; }

        [JsonProperty(JsonConstant.ACCESS_EXPIRATION_PROPERTY)]
        public int AccessExpiration { get; set; }

        [JsonProperty(JsonConstant.REFRESH_EXPIRATION_PROPERTY)]
        public int RefreshExpiration { get; set; }
    }
}

