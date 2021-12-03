using System;
using System.Collections.Generic;
using System.Text;
using Errands.Domain.Models;

namespace Errands.Application.Common.Services
{
    public class ListErrandsServiceModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
        public List<FileModel> FileModels { get; set; }

        public string UserId { get; set; }
    }
}
