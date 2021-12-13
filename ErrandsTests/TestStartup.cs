using Errands.Domain.Models;
using Errands.Mvc;
using Errands.Mvc.Services;
using ErrandsTests.FakeDependencies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTested.AspNetCore.Mvc;

using Errands.Data.Services;

namespace ErrandsTests
{

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.ReplaceTransient<IDateTimeProvider>(_ => DateTimeProviderMock.Create);
            //services.Replace<UserManager<User>, FakeUserManager>(ServiceLifetime.Scoped);
            //services.Replace<SignInManager<User>, FakeSignInManager>(ServiceLifetime.Scoped);
        }
    }
}
