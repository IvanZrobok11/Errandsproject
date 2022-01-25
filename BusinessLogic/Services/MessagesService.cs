using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Errands.Data.Services;
using Errands.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class MessagesService : IMessagesService
    {
        public MessagesService(IMessagesRepository repo)
        {
            _messagesRepository = repo;
        }

        private readonly IMessagesRepository _messagesRepository;
 



        public async Task<Chat> GetChatAsync(string userId1, string userId2)
        {
            var chats = _messagesRepository.Chats
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

        public async Task<Chat> GetChatByUsersIdAsync(string userId1, string userId2)
        {
            var chats = _messagesRepository.Chats
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
            return await _messagesRepository.CreateChatAsync(userId1, userId2);
        }

        public async Task SaveMessage(Message message)
        {
            await _messagesRepository.SaveMessage(message);
        }

        public IEnumerable<Message> GetMessages(Guid chatId)
        {
            return _messagesRepository.GetMessages(chatId);
        }
    }
}
