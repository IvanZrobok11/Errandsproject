using Errands.Data.Services.Configuration;
using Errands.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Errands.Data.Services
{
    public class ErrandsDbContext : IdentityDbContext<User>
    {
        public ErrandsDbContext(DbContextOptions<ErrandsDbContext> options) : base(options)
        {

        }
        public DbSet<Errand> Errands { get; set; }
        public DbSet<FileModel> FileModels { get; set; }
        public DbSet<Logo> Logos { get; set; }
        public DbSet<BlockedUser> BlockedUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new ErrandConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new FileModelConfiguration());
            base.OnModelCreating(builder);


        }

    }
}
