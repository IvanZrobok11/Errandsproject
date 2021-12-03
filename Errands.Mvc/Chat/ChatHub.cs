using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Errands.Data.Services;
using Errands.Domain.Models;

namespace Errands.Mvc.Chat
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task Send(string message, string to, Guid chatId)
        {
            var userName = Context.User.Identity.Name;
            await _messageService.SaveMessage(new Message
            {
                Content = message, 
                DateSend = DateTime.UtcNow, 
                ChatId = chatId, 
                SenderId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value
            });

            if (Context.UserIdentifier != to) 
                await Clients.User(Context.UserIdentifier)
                    .SendAsync("Receive", message, userName);
            await Clients.User(to).SendAsync("Receive", message, userName);
        }
    }
}
