using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Errands.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Errands.Data.Services
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ErrandsDbContext _dbContext;

        public MessageRepository(ErrandsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Chat> GetChatByUsersIdAsync(string userId1, string userId2)
        {
            var chats = _dbContext.Chats
                .Include(uch => uch.UsersChat)
                .ThenInclude(u => u.Interlocutor)
                .Where(ch => ch.UsersChat.Any(u => u.UserId == userId1))
                .Where(ch => ch.UsersChat.Any(u => u.UserId == userId2));

            Chat chat = await chats
                .FirstOrDefaultAsync(c => 
                    c.UsersChat.Any(u => u.UserId == userId1) &&
                    c.UsersChat.Any(u => u.UserId == userId2)); 
            return chat;
        }

        public async Task<Chat> CreateChatAsync(string userId1, string userId2)
        {
            var chat = new Chat();
            chat.UsersChat.Add(new UserChat {Chat = chat, UserId = userId1 });
            chat.UsersChat.Add(new UserChat { Chat = chat, UserId = userId2 });
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
