using Errands.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Errands.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IErrandsRepository _repository;

        public HomeController(IErrandsRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View(_repository.Errands);
        }
    }
}
