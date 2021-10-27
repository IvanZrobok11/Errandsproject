using Errands.Data.Services;
using Errands.Domain.Models;
using Errands.Mvc.Services;
using Errrands.Application.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Errands.Mvc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            GetConfiguration = configuration;
        }
        public IConfiguration GetConfiguration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ErrandsDbContext>(options =>
                options.UseSqlServer(GetConfiguration["DbConnection"]));

            services.AddIdentity<User, IdentityRole>(opts =>
                {
                    opts.Password.RequiredLength = 5;  
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false; 
                    opts.Password.RequireUppercase = false; 
                    opts.Password.RequireDigit = false; 
                })
                .AddEntityFrameworkStores<ErrandsDbContext>()
                .AddDefaultTokenProviders()
                ;
            services.AddTransient<IErrandsRepository, ErrandsRepository>();
            services.AddTransient<IFileProfile, BoxFile>();
            services.AddTransient<LogoImageProfile>();
            services.AddTransient<UserRepository>();
            services.AddTransient<FileServices>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
