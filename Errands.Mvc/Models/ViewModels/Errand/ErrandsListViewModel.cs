using Errands.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Errands.Mvc.Models.ViewModels
{
    public class ErrandsListViewModel
    {
        public IEnumerable<Errand> Errands { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    public class PageInfo
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
