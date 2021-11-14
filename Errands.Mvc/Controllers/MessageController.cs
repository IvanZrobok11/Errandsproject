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
using Errands.Mvc.Models;

namespace Errands.Mvc.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMessageRepository _messageRepository;

        public MessageController(IUserRepository userRepository, UserManager<User> userManager, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _messageRepository = messageRepository;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ViewResult> Chat(string receiverUserId)
        {
            User user = await _userRepository.GetUserInfoAsync(receiverUserId);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var chat = await _messageRepository.GetChatByUsersIdAsync(receiverUserId, userId);
            var model = new ChatDataViewModel
            {
                ReceiverUserInfo = new ReceiverUserInfoViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Nickname = user.UserName,
                    PathLogo = user.Logo?.Path,
                    Token = userId,
                    ReceiverUserId = user.Id,
                    ChatId = chat.Id
                },
                GetMessages = _messageRepository.GetMessages(chat.Id).TakeLast(100).ToList()
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
