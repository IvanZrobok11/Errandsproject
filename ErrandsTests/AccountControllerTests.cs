using Errands.Domain.Models;
using Errands.Mvc.Controllers;
using Errands.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ErrandsTests
{
    public class AccountControllerTests 
    {
        [Fact]
        public async Task RegisterNewUser_ReturnsRedirectToAction()
        {
            //Arrange
            var context = new Mock<HttpContext>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            contextAccessor.Setup(x => x.HttpContext).Returns(context.Object);

            var mockUserManager = new FakeUserManager(IdentityResult.Success);
            var mockSingInManager = new FakeSignInManager(contextAccessor.Object);
            
            var controller = new AccountController(mockSingInManager, mockUserManager);

            //Act
            var input = new NewUserInputBuilder().Build();
            var result = await controller.Register(input);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }
        [Fact]
        public async Task RegisterNewUser_ReturnsViewModel()
        {
            //Arrange
            var context = new Mock<HttpContext>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            contextAccessor.Setup(x => x.HttpContext).Returns(context.Object);

            var mockUserManager = new FakeUserManager(IdentityResult.Failed());
            var mockSingInManager = new FakeSignInManager(contextAccessor.Object);

            var controller = new AccountController(mockSingInManager, mockUserManager);

            //Act
            var input = new NewUserInputBuilder().Build();
            var result = await controller.Register(input) as ViewResult;
            var model = Assert.IsType<RegisterViewModel>(result.ViewData.Model);

            //Assert
            var returns = Assert.IsType<ViewResult>(result);
            Assert.Equal(new NewUserInputBuilder().Build().UserName, model.UserName);
            Assert.Equal(new NewUserInputBuilder().Build().Email, model.Email);
        }
    }
    public class FakeUserManager : UserManager<User>
    {
        public IdentityResult SetIdentityResult { get; set; }
        public FakeUserManager(IdentityResult result)
            : base(new Mock<IUserStore<User>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<User>>().Object,
                  new IUserValidator<User>[0],
                  new IPasswordValidator<User>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<User>>>().Object)
        {
            SetIdentityResult = result;
        }

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            return Task.FromResult(SetIdentityResult);
        }
    }
    public class FakeSignInManager : SignInManager<User>
    {
        public FakeSignInManager(IHttpContextAccessor contextAccessor)
            : base(
                  new FakeUserManager(IdentityResult.Success),
                  contextAccessor,
                  new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<ILogger<SignInManager<User>>>().Object,
                  new Mock<IAuthenticationSchemeProvider>().Object,
                  new Mock<IUserConfirmation<User>>().Object)
        {
        }

        public override Task SignInAsync(User user, bool isPersistent, string authenticationMethod = null)
        {
            return Task.FromResult(0);
        }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return Task.FromResult(SignInResult.Success);
        }

        public override Task SignOutAsync()
        {
            return Task.FromResult(0);
        }
    }
    public class NewUserInputBuilder
    {
        private string UserName { get; set; }
        private string Password { get; set; }
        private string ConfirmPassword { get; set; }
        private string Email { get; set; }

        internal NewUserInputBuilder()
        {
            this.UserName = "user@gamil.com";
            this.Password = "password";
            this.ConfirmPassword = "password";
            this.Email = "email@gamil.com";
        }

        internal NewUserInputBuilder WithNoUsername()
        {
            this.UserName = "";
            return this;
        }

        internal NewUserInputBuilder WithMismatchedPasswordConfirmation()
        {
            this.ConfirmPassword = "MismatchedPassword";
            return this;
        }

        internal RegisterViewModel Build()
        {
            return new RegisterViewModel
            {
                UserName = this.UserName,
                Password = this.Password,
                ConfirmPassword = this.ConfirmPassword,
                Email = this.Email
            };
        }
    }
}
