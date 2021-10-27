using Errands.Domain.Models;
using Errands.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Errands.Data.Services;
using SixLabors.ImageSharp;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Errrands.Application.Common;
using System.Linq;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using Errands.Mvc.Services;
using System;

namespace Errands.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public readonly UserRepository _userRepository;
        private readonly FileServices _fileServices;

        public UserController(SignInManager<User> signInManager, UserManager<User> userManager,
            UserRepository userRepository, FileServices fileServices)
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
            var path = _userRepository.GetLogoPathAsync(userId);

            return View(new UserViewModel
            {
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
            var path = _userRepository.GetLogoPathAsync(userId);

            return View(new UserViewModel
            {
                UserName = user.UserName, FirstName = user.FirstName, LastName = user.LastName, Path = path
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
        public async Task<IActionResult> ChangeLogo(IFormFile file)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (file == null)
            {
                return RedirectToAction("ChangeInfo");
            }
            try
            {
                var fileModel = _fileServices.SaveLogo(file);
                fileModel.User = user;
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
            return View("Profile");
        }
        
    }
}
