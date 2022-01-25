using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Errands.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Errands.Data.Services
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly ErrandsDbContext _dbContext;

        public MessagesRepository(ErrandsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Chat> CreateChatAsync(params string[] usersId)
        {
            var chat = new Chat();
            foreach (var id in usersId)
            {
                chat.UsersChat.Add(new UserChat { Chat = chat, UserId = id });
            }
            _dbContext.Chats.Add(chat);
            await _dbContext.SaveChangesAsync();
            return chat;
        }
        public async Task SaveMessage(Message message)
        {
            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();
        }
        public IEnumerable<Message> GetMessages(Guid chatId)
        {
            var result = _dbContext.Messages.Where(ch => ch.ChatId == chatId);
            return result;
        }

    }
}
