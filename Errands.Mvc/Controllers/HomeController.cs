using Errands.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Errands.Mvc.Models.ViewModels;

namespace Errands.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IErrandsRepository _repository;
        public int ItemsPerPage = 8;
        public HomeController(IErrandsRepository repository)
        {
            _repository = repository;
        }
        public ViewResult Index(int pageNumber = 1)
        {
            var list = _repository.Errands 
                .OrderByDescending(d => d.CreationDate)
                .Skip((pageNumber - 1) * ItemsPerPage)
                .Take(ItemsPerPage);

            //list.Reverse();
            return View(new ErrandsListViewModel 
            { 
                Errands = list,
                PageInfo = new PageInfo
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = this.ItemsPerPage,
                    TotalItems = _repository.Errands.Count()
                }
            });
        }
    }
}
