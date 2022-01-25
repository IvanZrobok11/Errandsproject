using Errands.Data.Services;
using Errands.Mvc.Models.ViewModels;
using Errands.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;

namespace Errands.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IErrandsService _errandService;
        public int ItemsPerPage = ControllerConstants.ItemPerMainPage;
        public HomeController(IErrandsService errandService)
        {
            _errandService = errandService;
        }
        public async Task<ViewResult> Index([FromQuery]int pageNumber = 1)
        {
            var list = await _errandService.AllAsync(pageNumber, ItemsPerPage);

            return View(new ListErrandsViewModel
            {
                Errands = list,
                PageInfo = new PageInfo
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = this.ItemsPerPage,
                    TotalItems = await _errandService.Total()
                }
            });
        }
    }
}
