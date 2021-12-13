using System;
using Errands.Data.Services;
using Errands.Domain.Models;
using Errands.Mvc.Chat;
using Errands.Mvc.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using AutoMapper;
using Errands.Application.Common.Services;
using Errands.Mvc.Extensions;
using Errands.Mvc.Middleware;

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
                    opts.Password.RequiredLength = 8;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<ErrandsDbContext>()
                .AddDefaultTokenProviders()
                ;
            
            services.AddAutoMapper(typeof(ServiceMappingProfile).Assembly);

            services.AddSignalR().AddHubOptions<ChatHub>(conf =>
            {
                conf.EnableDetailedErrors = true;
            });

            services.AddTransient<IErrandsService, ErrandsService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IMessageService, MessageService>()
                .AddTransient<IDateTimeProvider, DateTimeProvider>()
                .AddTransient<IFileServices ,FileServices>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error");
            }

            app.UseErrorHandle();
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/messages");
                endpoints.MapControllerRoute("default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
