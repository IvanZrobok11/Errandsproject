using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Domain.Models
{
    public class Logo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
