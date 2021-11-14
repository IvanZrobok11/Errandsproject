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
        public ReceiverUserInfoViewModel ReceiverUserInfo { get; set; }
        public List<Message> GetMessages { get; set; }
    }
}
