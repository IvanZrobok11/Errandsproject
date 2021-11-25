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
            User receiverUser = await _userRepository.GetUserInfoAsync(receiverUserId);
            User senderUser = await _userRepository.GetUserInfoAsync(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var chat = await _messageRepository.GetChatByUsersIdAsync(receiverUser.Id, senderUser.Id);
            var model = new ChatDataViewModel
            {
                ChatId = chat.Id,
                ReceiverUser = receiverUser,
                SenderUser = senderUser
            };
            return View(model);
        }

        [HttpPost]
        public JsonResult LoadMessages(Guid chatId, int countMessages, int skipMessages)
        {
            
            var messages = _messageRepository.GetMessages(chatId)
                .SkipLast(skipMessages).TakeLast(countMessages);
            return Json(messages.ToArray());
        }


        [HttpGet]
        public IActionResult MessageList()
        {
            return View();
        }
    }
}
