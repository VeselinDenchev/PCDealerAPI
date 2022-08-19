namespace Data.Services.ViewModels
{
    using Newtonsoft.Json;

    public class LoginViewModel
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
