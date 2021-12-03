using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Errands.Domain.Models;

namespace Errands.Application.Common.Services
{
    public class EditErrandModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Desc { get; set; }
        public decimal Cost { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<FileModel> File { get; set; }
    }
}
