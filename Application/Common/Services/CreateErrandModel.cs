using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Errands.Application.Common.Services
{
    public class CreateErrandModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Desc { get; set; }
        public decimal Cost { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
