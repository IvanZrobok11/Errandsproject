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
using Errands.Mvc.Extensions;
using Errands.Mvc.Models;
using Errands.Mvc.Services;

namespace Errands.Mvc.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public MessageController(IUserService userService, IMessageService messageService)
        {
            _userService = userService;
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
        public async Task<IActionResult> InformAboutTakenErrands([FromServices] IErrandsService errandsService)
        {
            var myErrands = await errandsService.GetUnfinishedErrands(User.GetId());
            return View(myErrands);
        }
        [HttpPost]
        public IActionResult InformAboutTakenErrands(int i)
        {
            return View();
        }
    }
}
