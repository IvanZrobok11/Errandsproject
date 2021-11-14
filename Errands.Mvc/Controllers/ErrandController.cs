using Errands.Data.Services;
using Errands.Domain.Models;
using Errands.Mvc.Models.ViewModels;
using Errands.Mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Errands.Mvc.Controllers
{
    [Authorize]
    public class ErrandController : Controller
    {
        private readonly IErrandsRepository _errandRepository;
        private readonly FileServices _fileServices;
        private readonly UserManager<User> _userManager;
        private readonly IMessageRepository _messageRepository;

        public ErrandController(IErrandsRepository errandRepository, FileServices fileServices,
            UserManager<User> userManager, IMessageRepository messageRepository)
        {
            _errandRepository = errandRepository;
            _fileServices = fileServices;
            _userManager = userManager;
            _messageRepository = messageRepository;
        }
        //[HttpGet]
        //public ViewResult Getdata()
        //{
        //    List<string> list = new List<string>();
        //    string token = User.Identity.AuthenticationType;
        //    string claim = User.Claims.ToString();
        //    string cl = ClaimsIdentity.DefaultNameClaimType;
        //    string name = User.Identity.Name;
        //    //ValidateAntiForgeryTokenAttribute v = to
           
        //    list.Add(token);
        //    list.Add(cl);
        //    list.Add(claim);
        //    list.Add(name);
        //    return View(list);  
        //}
        [HttpGet]
        public ViewResult ListMyErrand()
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<GetMyErrandViewModel> errands = _errandRepository.GetErrandsByUserId(userid).Select(x => new GetMyErrandViewModel
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
        public ViewResult ListErrandToDo()
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<GetErrandToDoViewModel> errands = _errandRepository.GetErrandsByHelperUserId(userid).Select(x => new GetErrandToDoViewModel
            {
                Id = x.Id,
                Cost = x.Cost,
                Description = x.Description,
                Title = x.Title,
                Done = x.Done,
                NeedlyUserId = x.UserId
            });

            return View(errands);
        }
        [HttpGet]
        public async Task<VirtualFileResult> GetFile(Guid id)
        {
            var file = await _errandRepository.GetFileByIdAsync(id);
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
            await _errandRepository.CreateErrandAsync(errand, files);
            return RedirectToAction(nameof(ListMyErrand));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var errand = await _errandRepository.GetErrandByIdAsync(id);
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
            await _errandRepository.UpdateAsync(errand);
            return RedirectToAction(nameof(ListMyErrand));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var errand = await _errandRepository.GetErrandByIdAsync(id);
            try
            {
                var paths = errand.FileModels.Select(p => p.Path);
                foreach (var p in paths)
                {
                    _fileServices.DeleteFile(p);
                }
                await _errandRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(ListMyErrand));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Take(Guid id)
        {
            var errand = await _errandRepository.GetErrandByIdAsync(id);

            errand.Active = false;
            errand.HelperUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var chat = await _messageRepository.GetChatByUsersIdAsync(errand.HelperUserId, errand.UserId);
            if (chat == null)
            {
                chat = await _messageRepository.CreateChatAsync(errand.UserId, errand.HelperUserId);
            }
            await _errandRepository.UpdateAsync(errand);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var errand = await _errandRepository.GetErrandByIdAsync(id);
            errand.Done = true;
            
            await _errandRepository.UpdateAsync(errand);
            return RedirectToAction("Index", "Home");
        }
    }
}
