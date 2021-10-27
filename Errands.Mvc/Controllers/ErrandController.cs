using Errands.Data.Services;
using Errands.Domain.Models;
using Errands.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Errands.Application.Exceptions;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Errrands.Application.Common;
using Errands.Mvc.Services;

namespace Errands.Mvc.Controllers
{
    [Authorize]
    public class ErrandController : Controller
    {
        private readonly IErrandsRepository _repository;
        private readonly FileServices _fileServices;

        public ErrandController(IErrandsRepository repository, FileServices fileServices)
        {
            _repository = repository;
            _fileServices = fileServices;
        }
        [HttpGet]
        public IActionResult List()
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<ErrandGetViewModel> errands = _repository.GetErrandsByUserId(userid).Select(x => new ErrandGetViewModel 
            { 
                Id = x.Id,
                Cost = x.Cost,
                Description = x.Description,
                Title = x.Title, 
                Done = x.Done,
                Active = x.Active,
                HelperUserId = x.HelperUserId
            });
            
            return View(errands);
        }
        [HttpGet]
        public VirtualFileResult GetFile(Guid id)
        {
            var file = _repository.GetFileById(id);
            return File($"{file.Path}", "AppContext/pdf", file.Name);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ErrandCreateViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ErrandCreateViewModel model)
        {
            var errand = new Errand
            {
                Title = model.Title,
                Description = model.Desc,
                Cost = model.Cost,
                CreationDate = DateTime.Now,
                UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value,             
            };
            var files = new List<FileModel>();
            if (model.Files != null)
            {
                foreach (var uploadedFile in model.Files)
                {
                    try
                    {
                        var fileModel = await _fileServices.SaveFile(uploadedFile);
                        fileModel.Errand = errand;
                        files.Add(fileModel);                         
                    }
                    catch (Exception e)
                    {
                        TempData["errorMessage"] = e.Message;
                        return View(model);
                    }
                   
                }
            }
            await _repository.CreateErrandAsync(errand, files);
            return RedirectToAction("List");
        }
        
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var errand = _repository.GetErrandById(id);
            if (errand == null)
            {
                return NotFound();
            }
            var model = new ErrandEditViewModel
            {
                Title = errand.Title,
                Desc = errand.Description,
                Cost = errand.Cost,
                Id = errand.Id,
                File = errand.FileModels    
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ErrandEditViewModel model)
        {
            var errand = new Errand
            {
                Title = model.Title,
                Description = model.Desc,
                Cost = model.Cost,
                Id = model.Id               
            };
            await _repository.UpdateAsync(errand);
            return RedirectToAction("List");   
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var errand = _repository.GetErrandById(id);
            try
            {
                var paths = errand.FileModels.Select(p => p.Path);             
                foreach (var p in paths)
                {
                    _fileServices.DeleteFile(p);
                }
                await _repository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            
            return RedirectToAction("List");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Take(Guid id)
        {
            var errand = _repository.GetErrandById(id);

            var newErrand = errand;
            newErrand.Active = false;
            newErrand.HelperUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await _repository.UpdateAsync(newErrand);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var errand = _repository.GetErrandById(id);

            var newErrand = errand;
            newErrand.Done = true;          

            await _repository.UpdateAsync(newErrand);
            return RedirectToAction("Index", "Home");
        }
    }
}
