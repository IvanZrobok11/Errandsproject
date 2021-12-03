using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Application.Common.Services
{
    public class GetMyErrandServiceModel
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool Done { get; set; }
        public string HelperUserId { get; set; }
    }
}
