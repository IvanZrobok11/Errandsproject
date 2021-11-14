using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;

namespace Errands.Domain.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime DateSend { get; set; }
        public string SenderName { get; set; }

        public Guid ChatId { get; set; }
        public Chat Chatting { get; set; }
    }
}
