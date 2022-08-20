namespace Data.Services.EntityServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        public UserService(PcDealerDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public PcDealerDbContext DbContext { get; init; }

        public User GetUserByUserName(string userName)
        {
            User user = this.DbContext.Users.Where(u => u.UserName == userName).FirstOrDefault();

            if (user is null) return null;

            this.DbContext.Entry(user).State = EntityState.Detached;

            return user;
        }
    }
}
