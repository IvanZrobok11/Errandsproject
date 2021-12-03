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
using AutoMapper;
using Errands.Mvc.Models;

namespace Errands.Mvc.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public MessageController(IUserService userService, IMapper mapper, IMessageService messageService)
        {
            _userService = userService;
            _mapper = mapper;
            _messageService = messageService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ViewResult> Chat(string receiverUserId)
        {
            User receiverUser = await _userService.GetUserInfoAsync(receiverUserId);
            User senderUser = await _userService.GetUserInfoAsync(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var chat = await _messageService.GetChatByUsersIdAsync(receiverUser.Id, senderUser.Id);

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
            
            var messages = _messageService.GetMessages(chatId)
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
