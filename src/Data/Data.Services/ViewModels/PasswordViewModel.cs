namespace Data.Services.ViewModels
{
    using Constants;

    using Newtonsoft.Json;

    public class PasswordViewModel
    {
        [JsonProperty(PropertyName = JsonConstant.CURRENT_PASSWORD_PROPERTY)]
        public string CurrentPassword { get; set; }

        [JsonProperty(PropertyName = JsonConstant.NEW_PASSWORD_PROPERTY)]
        public string NewPassword { get; set; }
    }
}
