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
using ErrandsTests.FakeDependencies;
using Microsoft.Extensions.Caching.Memory;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace ErrandsTests
{
    //public class AccountControllerTests
    //{
    //    private Mock<IHttpContextAccessor> _contextAccessor;
    //    private FakeUserManager _mockUserManager;
    //    private FakeSignInManager _mockSingInManager;

    //    public AccountControllerTests()
    //    {
    //        var context = new Mock<HttpContext>();
    //        _contextAccessor.Setup(x => x.HttpContext).Returns(context.Object);
    //        _mockSingInManager = new FakeSignInManager(_contextAccessor.Object);
    //        _mockUserManager = new FakeUserManager(IdentityResult.Failed());
    //        _contextAccessor = new Mock<IHttpContextAccessor>();
    //    }
    //    [Fact]
    //    public async Task RegisterNewUser_ReturnsRedirectToAction()
    //    {
    //        //Arrange
    //        var controller = new AccountController(_mockSingInManager, _mockUserManager);

    //        //Act
    //        var input = new NewUserInputBuilder().Build();
    //        var result = await controller.Register(input);

    //        //Assert
    //        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
    //        Assert.Equal("Index", redirectToActionResult.ActionName);

    //    }
    //    [Fact]
    //    public async Task RegisterNewUser_ReturnsViewModel()
    //    {
    //        //Arrange
            
    //        var controller = new AccountController(_mockSingInManager, _mockUserManager);

    //        //Act
    //        var input = new NewUserInputBuilder().Build();
    //        var result = await controller.Register(input) as ViewResult;
    //        var model = Assert.IsType<RegisterViewModel>(result.ViewData.Model);

    //        //Assert
    //        var returns = Assert.IsType<ViewResult>(result);
    //        Assert.Equal(new NewUserInputBuilder().Build().UserName, model.UserName);
    //        Assert.Equal(new NewUserInputBuilder().Build().Email, model.Email);
    //    }
    //}
    //public class NewUserInputBuilder
    //{
    //    private string UserName { get; set; }
    //    private string Password { get; set; }
    //    private string ConfirmPassword { get; set; }
    //    private string Email { get; set; }

    //    internal NewUserInputBuilder()
    //    {
    //        this.UserName = "user@gamil.com";
    //        this.Password = "password";
    //        this.ConfirmPassword = "password";
    //        this.Email = "email@gamil.com";
    //    }

    //    internal NewUserInputBuilder WithNoUsername()
    //    {
    //        this.UserName = "";
    //        return this;
    //    }

    //    internal NewUserInputBuilder WithMismatchedPasswordConfirmation()
    //    {
    //        this.ConfirmPassword = "MismatchedPassword";
    //        return this;
    //    }

    //    internal RegisterViewModel Build()
    //    {
    //        return new RegisterViewModel
    //        {
    //            UserName = this.UserName,
    //            Password = this.Password,
    //            ConfirmPassword = this.ConfirmPassword,
    //            Email = this.Email
    //        };
    //    }
    //}
    public class AccountControllerTests
    {
        [Fact]
        public void GetLogin_ShouldHave_AllowAnonymousFilter()
        {
            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(With.No<string>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .AllowingAnonymousRequests());
        }

        [Fact]
        public void AccountController_ShouldHave_AuthorizeAttribute()
        {
            MyMvc.Controller<AccountController>()
                .ShouldHave().Attributes(a => 
                    a.RestrictingForAuthorizedRequests());
        }
       

        [Fact]
        public void PostLogin_ShouldReturn_DefaultViewWithInvalidModel()
        {
            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(
                    With.Default<LoginViewModel>()
                   ))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<LoginViewModel>()
                    .ContainingErrorFor(m => m.Email)
                    .ContainingErrorFor(m => m.Password))
                .AndAlso()
                .ShouldReturn()
                .View();
        }



        [Fact]
        public void GetRegister_ShouldBe_RoutedCorrectly()
        {
            //MyMvc
            //    .Routing()
            //    .ShouldMap("/Account/Register")
            //    .To<CheckoutController>(c => c.AddressAndPayment());
        }
        [Fact]
        public void PostLoginShouldReturnRedirectToLocalWithValidUserNameAndReturnUrl()
        {
            var model = new LoginViewModel
            {
                Email = FakeSignInManager.ValidUser,
                Password = FakeSignInManager.ValidUser
            };

            var returnUrl = "/Home/Index";

            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(
                    model)
                    )
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToUrl(returnUrl));
        }

        

      
    }
}
