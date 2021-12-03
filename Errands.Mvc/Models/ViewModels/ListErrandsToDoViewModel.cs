﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Errands.Application.Common.Services;

namespace Errands.Mvc.Models.ViewModels
{
    public class ListErrandsToDoViewModel
    {
        public IEnumerable<GetErrandToDoServiceModel> Errands { get; set; } 
        public PageInfo PageInfo { get; set; }
    }
}
