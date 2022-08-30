namespace Data.Services.ViewModels
{
    using Constants;

    using Newtonsoft.Json;

    public class RegisterViewModel
    {
        [JsonProperty(PropertyName = JsonConstant.FIRST_NAME_PROPERTY)]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = JsonConstant.LAST_NAME_PROPERTY)]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = JsonConstant.EMAIL_PROPERTY)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = JsonConstant.PHONE_NUMBER_PROPERTY)]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = JsonConstant.ADDRESS_PROPERTY)]
        public string Address { get; set; }

        [JsonProperty(PropertyName = JsonConstant.PASSWORD_PROPERTY)]
        public string Password { get; set; }
    }
}
