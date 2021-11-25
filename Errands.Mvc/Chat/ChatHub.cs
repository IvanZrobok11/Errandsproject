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
        private readonly IMessageRepository _messageRepository;

        public ChatHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task Send(string message, string to, Guid chatId)
        {
            var userName = Context.User.Identity.Name;
            await _messageRepository.SaveMessage(new Message
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
