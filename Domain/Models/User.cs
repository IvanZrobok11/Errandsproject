using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Domain.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            UserChats = new List<UserChat>();
        }
        public List<UserChat> UserChats { get; set; } 


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Logo Logo { get; set; }
        public ICollection<Errand> Errands { get; set; }

    }
}
