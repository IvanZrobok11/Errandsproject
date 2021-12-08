using Errands.Mvc.Controllers;
using Errands.Mvc.Models.ViewModels;
using ErrandsTests.FakeDependencies;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace ErrandsTests
{
    public class AccountControllerTests
    {
        [Fact]
        public void AccountController_ShouldHave_AuthorizeAttribute()
        {
            MyMvc.Controller<AccountController>()
                .ShouldHave().Attributes(a =>
                    a.RestrictingForAuthorizedRequests());
        }
        
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
        public void GetLogin_ShouldHave_ReturnCorrectViewWithModel_WithUrl()
        {
            string returnUrl = "/";
            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(
                    returnUrl))
                .ShouldReturn()
                .View(new LoginViewModel() { ReturnUrl = returnUrl });
        }
        [Fact]
        public void PostLogin_ShouldHave_AllowAnonymousFilter()
        {
            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(With.Default<LoginViewModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .AllowingAnonymousRequests());
        }
        [Fact]
        public void PostLogin_ShouldReturn_DefaultView_WithInvalidModel()
        {
            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(
                    With.Default<LoginViewModel>()))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<LoginViewModel>()
                    .ContainingErrorFor(m => m.UserName)
                    .ContainingErrorFor(m => m.Password))
                .AndAlso()
                .ShouldReturn()
                .View();
        }
        [Fact]
        public void PostLogin_ShouldReturn_RedirectToActionIfReturnUrlIsNull_WithValidUser()
        {
            var model = new LoginViewModel
            {
                UserName = FakeSignInManager.ValidUser,
                Password = FakeSignInManager.Password
            };
            MyMvc
              .Controller<AccountController>()
              .Calling(c => c.Login(model))
              .ShouldReturn()
              .Redirect(redirect =>
                  redirect.To<HomeController>(
                      c => c.Index(With.No<int>())
                      ));
        }
        [Fact]
        public void PostLogin_ShouldReturn_RedirectToUrl_WithValidUser()
        {
            var model = new LoginViewModel
            {
                UserName = FakeSignInManager.ValidUser,
                Password = FakeSignInManager.Password,
                ReturnUrl = "/Home/Index"
            };
            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(model))
                .ShouldReturn()
                .Redirect(redirect =>
                    redirect.ToUrl(model.ReturnUrl));
        }
        [Fact]
        public void PostLogin_ShouldReturn_ViewWithInvalidCredentials()
        {
            var model = new LoginViewModel()
            {
                UserName = "NameNamovich",
                Password = "Invalid",
            };
            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(model))
                .ShouldHave()
                .ModelState(ms => ms
                    .For<ValidationSummary>()
                    .ContainingError(string.Empty)
                    .ThatEquals("UserName or password is wrong"))
                .AndAlso()
                .ShouldReturn()
                .View(model);
        }

        [Fact]
        public void GetRegister_ShouldHave_AllowAnonymousFilter()
        {
            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(With.No<string>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .AllowingAnonymousRequests());
        }





    }
}
