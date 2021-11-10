using System;

namespace Errands.Mvc.Models.ViewModels
{
    public class GetMyErrandViewModel
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
