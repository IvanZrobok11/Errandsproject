using System;

namespace Errands.Mvc.Models.ViewModels
{
    public class GetErrandToDoViewModel
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public string NeedlyUserId { get; set; }
    }
}
