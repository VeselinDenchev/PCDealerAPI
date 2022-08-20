namespace Data.Services.ViewModels
{
    using Newtonsoft.Json;

    public class PasswordViewModel
    {
        [JsonProperty(PropertyName = "currentPassword")]
        public string CurrentPassword { get; set; }

        [JsonProperty(PropertyName = "newPassword")]
        public string NewPassword { get; set; }
    }
}
