using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Errands.Data.Services;
using Errands.Domain.Models;

namespace ErrandsTests.FakeDependencies
{
    public class FakeMessageService : IMessageService
    {
        public Task<Chat> CreateChatAsync(string userId1, string userId2)
        {
            throw new NotImplementedException();
        }

        public Task<Chat> GetChatByUsersIdAsync(string userId1, string userId2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetMessages(Guid chatId)
        {
            throw new NotImplementedException();
        }

        public Task SaveMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
