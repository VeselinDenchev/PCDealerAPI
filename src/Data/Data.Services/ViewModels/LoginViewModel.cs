namespace Data.Services.ViewModels
{
    using Constants;

    using Newtonsoft.Json;

    public class LoginViewModel
    {
        [JsonProperty(PropertyName = JsonConstant.EMAIL_PROPERTY)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = JsonConstant.PASSWORD_PROPERTY)]
        public string Password { get; set; }
    }
}
