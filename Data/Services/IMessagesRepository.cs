using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Errands.Domain.Models;

namespace Errands.Data.Services
{
    public interface IMessagesRepository
    {
        Task<Chat> CreateChatAsync(params string[] usersId);
        Task SaveMessage(Message message);
        IEnumerable<Message> GetMessages(Guid chatId);
        IQueryable<Chat> Chats { get; }
    }
}
