using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Errands.Domain.Models;
using Errands.Mvc.Models.ViewModels;

namespace Errands.Mvc.Models
{
    public class ChatDataViewModel
    {
        public User ReceiverUser { get; set; }
        public User SenderUser { get; set; }
        public Guid ChatId { get; set; }
    }
}
