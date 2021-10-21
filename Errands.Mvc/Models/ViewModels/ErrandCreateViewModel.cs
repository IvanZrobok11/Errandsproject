using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Errands.Mvc.Models.ViewModels
{
    public class ErrandCreateViewModel
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public decimal Cost { get; set; }
    }
}
