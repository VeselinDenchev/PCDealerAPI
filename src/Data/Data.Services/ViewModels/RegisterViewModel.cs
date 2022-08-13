namespace Data.Services.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        private const string CONFIRM_PASSWORD_STRING = "ConfirmPassword";

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Compare(CONFIRM_PASSWORD_STRING)]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
