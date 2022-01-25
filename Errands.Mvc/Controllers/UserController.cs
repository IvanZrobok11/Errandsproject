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
using AutoMapper;
using BusinessLogic.Interfaces;
using Errands.Application.Common.Services;
using Errands.Mvc.Extensions;
using Microsoft.AspNetCore.Routing;

namespace Errands.Mvc.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUsersService _usersService;
        private readonly IFileServices _fileServices;

        public UserController(IMapper mapper, UserManager<User> userManager,
            IUsersService usersService, IFileServices fileServices)
        {
            _mapper = mapper;
            _userManager = userManager;
            _usersService = usersService;
            _fileServices = fileServices;
        }
        [HttpGet]
        public async Task<IActionResult> Profile(string identity)
        {
            var userId = identity ?? this.User.GetId();

            User user = await _userManager.FindByIdAsync(userId);
            return View(_mapper.Map<UserProfileModel>(user));
        }
        [HttpGet]
        public async Task<IActionResult> ChangeInfo()
        {
            User user = await _userManager.FindByIdAsync(User.GetId());
            return View(_mapper.Map<UserProfileModel>(user));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeInfo(UserProfileModel profileModel)
        {
            if (!ModelState.IsValid)
            {
                return View(profileModel);
            }
            var changedInfo = await _userManager.FindByIdAsync(User.GetId());
            {
                changedInfo.UserName = profileModel.UserName;
                changedInfo.FirstName = profileModel.FirstName;
                changedInfo.LastName = profileModel.LastName;
            }
            var result = await _userManager.UpdateAsync(changedInfo);
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
            return View(profileModel);
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
            var path = await _usersService.GetLogoPathAsync(User.GetId());
            try
            {
                if (path != null)
                {
                    _fileServices.DeleteFile(path);
                    await _usersService.DeleteLogoAsync(User.GetId());
                }
                
                var logo = await _fileServices.SaveLogoAsync(Logo);
                logo.UserId = User.GetId();
                await _usersService.AddLogoAsync(logo);
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
