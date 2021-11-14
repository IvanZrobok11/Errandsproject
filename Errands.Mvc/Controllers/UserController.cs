using Errands.Data.Services;
using Errands.Domain.Models;
using Errands.Mvc.Models.ViewModels;
using Errands.Mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Errands.Mvc.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public readonly IUserRepository _userRepository;
        private readonly FileServices _fileServices;
        
        public UserController(SignInManager<User> signInManager, UserManager<User> userManager,
            IUserRepository userRepository, FileServices fileServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _fileServices = fileServices;
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {          
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(userId);
            var path = await _userRepository.GetLogoPathAsync(userId);

            return View(new UserViewModel
            {
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Path = path
            });
        }
        [HttpGet]
        public async Task<IActionResult> ChangeInfo()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(userId);
            var path = await _userRepository.GetLogoPathAsync(userId);

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
            else
            {
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
            }
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeLogo(IFormFile Logo)
        {
            if (Logo == null)
            {
                return View("ChangeInfo");
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(userId);
            var path = await _userRepository.GetLogoPathAsync(userId);
            try
            {
                if (path != null)
                {
                    _fileServices.DeleteFile(path);
                    await _userRepository.DeleteLogoAsync(userId);
                }
                var logo = _fileServices.SaveLogo(Logo);
                logo.User = user;
                await _userRepository.AddLogoAsync(logo);
            }
            catch (ImageProcessingException ex)
            {
                TempData["message"] = ex.ToString();
                return RedirectToAction("ChangeInfo");
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.ToString();
                return RedirectToAction("ChangeInfo");
            }
            return RedirectToAction("Profile");
        }

    }
}
