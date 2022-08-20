namespace Data.Services.EntityServices.Interfaces
{
    using Data.Models.Entities;

    public interface IUserService
    {
        public User GetUserByUserName(string userName);
    }
}
