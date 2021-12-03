using Errands.Application.Exceptions;
using Errands.Data.Services;
using Errands.Domain.Models;
using Errands.Mvc.Models.ViewModels;
using Errands.Mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Errands.Mvc.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IFileServices _fileServices;

        public UserController(SignInManager<User> signInManager, UserManager<User> userManager,
            IUserService userService, IFileServices fileServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
            _fileServices = fileServices;
        }
        [HttpGet]
        public async Task<IActionResult> Profile(string identity)
        {
            string userId = identity;
            if (identity == null)
            {
                userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            User user = await _userManager.FindByIdAsync(userId);
            var path = await _userService.GetLogoPathAsync(userId);

            return View(new UserViewModel
            {
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Path = path,
                CompletedErrands = user.CompletedErrands,
                Id = user.Id,
            });
        }
        [HttpGet]
        public async Task<IActionResult> ChangeInfo()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(userId);
            
            var path = await _userService.GetLogoPathAsync(userId);

            return View(new UserViewModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Path = path
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeInfo(UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            var newInfoAboutUser = user;
            newInfoAboutUser.UserName = viewModel.UserName;
            newInfoAboutUser.FirstName = viewModel.FirstName;
            newInfoAboutUser.LastName = viewModel.LastName;

            var result = await _userManager.UpdateAsync(newInfoAboutUser);
            if (result.Succeeded)
            {
                return RedirectToAction("Profile");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeLogo(IFormFile Logo)
        {
            if (Logo == null)
            {
                TempData["message"] = "Please attach file";
                return RedirectToAction("ChangeInfo");
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(userId);
            var path = await _userService.GetLogoPathAsync(userId);
            try
            {
                if (path != null)
                {
                    _fileServices.DeleteFile(path);
                    await _userService.DeleteLogoAsync(userId);
                }
                var logo = await _fileServices.SaveLogoAsync(Logo);
                logo.User = user;
                await _userService.AddLogoAsync(logo);
            }
            catch (WrongExtensionFileException)
            {
                TempData["errorMessage"] = "File have unallowable extension";
                return RedirectToAction("ChangeInfo");
            }
            catch (InvalidSizeFileException)
            {
                TempData["errorMessage"] = "Image size is bigger than allowed";
                return RedirectToAction("ChangeInfo");
            }
            catch (ImageProcessingException)
            {
                TempData["message"] = "Processing exception";
                return RedirectToAction("ChangeInfo");
            }
            return RedirectToAction("Profile");
        }
    }
}
