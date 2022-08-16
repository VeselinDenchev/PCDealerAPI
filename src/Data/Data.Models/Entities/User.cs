namespace Data.Models.Entities
{
    using Data.Models.Abstractions.Interfaces;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser, IUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        //public ICollection<Order> Orders { get; set; }
    }
}
