using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Application.Common.Services
{
    public class GetErrandToDoServiceModel
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public string NeedlyUserId { get; set; }
    }
}
