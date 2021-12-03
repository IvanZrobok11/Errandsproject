using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Errands.Application.Common.Services;
using Errands.Domain.Models;

namespace Errands.Mvc.Models.ViewModels
{
    public class ListErrandsViewModel
    {
        public IEnumerable<ListErrandsServiceModel> Errands { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    
}
