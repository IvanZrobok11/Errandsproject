using Errands.Domain.Models;
using Errands.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Errands.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Errands.Mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, 
            ILogger<AccountController> logger)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            _logger = logger;
        }
        public SignInManager<User> SignInManager { get; }
        public UserManager<User> UserManager { get; }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                // 
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                     _logger.LogInformation("Logged in {username}.", model.UserName);
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            _logger.LogWarning("Failed to log in {userName}.", model.UserName);
            ModelState.AddModelError("", "UserName or password is wrong");
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                User user = new User { Email = model.Email, UserName = model.UserName };
                // 
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User {username} was created", model.UserName);
                    await SignInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result, "Error creating user:");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            _logger.LogInformation("Logged out {username} ", this.User.Identity.Name);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult ResetPassword(string id)
        {
            var resetPasswordViewModel = new ResetPasswordViewModel() { Id = id };
            if (id == null)
            {
                return RedirectToAction("Error", "Error");
            }
            else
            {
                return View(resetPasswordViewModel);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                var result = await UserManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    return RedirectToAction("Profile","User");
                }
                else
                {
                    AddErrors(result, "Error reset password: ");
                }
            }
            return View(model);
        }
        private void AddErrors(IdentityResult result, string loggerMessage)
        {
            foreach (var error in result.Errors)
            {
                _logger.LogWarning(loggerMessage + "{error}", error.Description);
                ModelState.AddModelError("", error.Description);
            }
        }

    
    }
}
