using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Domain.Models
{
    public class BlockedUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
