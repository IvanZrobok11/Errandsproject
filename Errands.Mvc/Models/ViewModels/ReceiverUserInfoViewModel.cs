using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Errands.Mvc.Models.ViewModels
{
    public class ReceiverUserInfoViewModel
    {
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ReceiverUserId { get; set; }
        public string Token { get; set; }
        public string PathLogo { get; set; }
        public Guid ChatId { get; set; }
    }
}
