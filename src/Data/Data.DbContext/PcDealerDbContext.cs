namespace Data.DbContext
{
    using System.Reflection;

    using Data.Models.Abstractions.Interfaces.Base;
    using Data.Models.Entities;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class PcDealerDbContext : IdentityDbContext<User>
    {
        public DbSet<Brand> Brands { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ImageFile> ImageFiles { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }
        
        public DbSet<Review> Reviews { get; set; }


        private IConfigurationRoot configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            configuration = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries<IEntity<string>>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAtUtc = DateTime.UtcNow;
                    entity.Entity.ModifiedAtUtc = DateTime.UtcNow;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.ModifiedAtUtc = DateTime.UtcNow;
                }
                else if (entity.State == EntityState.Detached)
                {
                    entity.State = EntityState.Deleted;
                    entity.Entity.DeletedAtUtc = DateTime.UtcNow;
                    entity.Entity.IsDeleted = true;
                }
            }

            return base.SaveChanges();
        }
    }
}