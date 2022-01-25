using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Errands.Domain.Models;

namespace BusinessLogic.Interfaces
{
    public interface IMessagesService
    {
        Task<Chat> GetChatByUsersIdAsync(string userId1, string userId2);
        Task<Chat> CreateChatAsync(string userId1, string userId2);
        Task SaveMessage(Message message);
        IEnumerable<Message> GetMessages(Guid chatId);
    }
}
