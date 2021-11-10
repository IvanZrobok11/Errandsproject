using Errands.Data.Services;
using Errands.Domain.Models;
using Errands.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Errands.Mvc.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly UserRepository _repository;
        private readonly UserManager<User> _userManager;
        public MessageController(UserRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ViewResult> Chat(string receiverUserId)
        {
            User receiverUserInfo = await _repository.GetUserInfoAsync(receiverUserId);
            //User chatterUserInfo = await _userManager.FindByIdAsync(userId);
            var model = new UserInfoViewModel
            {
                FirstName = receiverUserInfo.FirstName,
                LastName = receiverUserInfo.LastName,
                Nickname = receiverUserInfo.UserName,
                PathLogo = receiverUserInfo.Logo?.Path,
                Token = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value,
                ReciverUserId = receiverUserInfo.Id
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult MessageList()
        {
            return View();
        }
    }
}
