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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ErrandConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }

    }
}
