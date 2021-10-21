using System;

namespace Errands.Domain.Models
{
    public class Errand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public bool Done { get; set; }  
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public string HelperUserId { get; set; }

        public string UserId { get; set; }// foreingkey 
        public User User { get; set; }//navigation property
    }
}
