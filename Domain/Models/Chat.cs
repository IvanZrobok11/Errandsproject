using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Domain.Models
{
    public class Chat
    {
        public Chat()
        {
            this.UsersChat = new List<UserChat>();
        }
        public List<Message> Messages { get; set; }
        public Guid Id { get; set; }

        public List<UserChat> UsersChat { get; set; }
    }

    public class UserChat
    {
        public string UserId { get; set; }
        public User Interlocutor { get; set; }

        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
