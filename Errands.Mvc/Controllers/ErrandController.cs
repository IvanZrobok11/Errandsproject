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
using Errands.Application.Exceptions;
using Errrands.Application.Common.Extension;
using SixLabors.ImageSharp;

namespace Errands.Mvc.Controllers
{
    [Authorize]
    public class ErrandController : Controller
    {
        private readonly IErrandsRepository _errandRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly FileServices _fileServices;
        private readonly UserManager<User> _userManager;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ErrandController(
            IErrandsRepository errandRepository, 
            FileServices fileServices,
            UserManager<User> userManager, 
            IMessageRepository messageRepository, 
            IDateTimeProvider dateTimeProvider)
        {
            _errandRepository = errandRepository;
            _fileServices = fileServices;
            _userManager = userManager;
            _messageRepository = messageRepository;
            _dateTimeProvider = dateTimeProvider;
        }
        //}
        
        [HttpGet]
        public async Task<ViewResult> GetErrand(Guid errandId)
        {
            Errand errand = await _errandRepository.GetErrandByIdAsync(errandId);
            return View(errand);
        }
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
            IEnumerable<GetErrandToDoViewModel> errands = _errandRepository.GetErrandsByHelperUserId(userid)
                .Select(x => new GetErrandToDoViewModel
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
            return File($"{file.Path.PathToUrl()}", "AppContext/pdf", file.Name);
            
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
                CreationDate = _dateTimeProvider.Now(),
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
                    catch (WrongExtensionFileException)
                    {
                        TempData["errorMessage"] = "File have unallowable extension";
                        return View(model);
                    }
                    catch (InvalidSizeFileException)
                    {
                        TempData["errorMessage"] = "File size is bigger than allowed";
                        return View(model);
                    }
                    catch (ImageProcessingException)
                    {
                        TempData["errorMessage"] = "Processing exception";
                        return View(model);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
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
                throw new NotFoundException(errand?.Title, id);
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
            return RedirectToAction("ListErrandToDo");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var errand = await _errandRepository.GetErrandByIdAsync(id);
            if (errand.HelperUserId == null || 
                errand.Done == true)
            {
                return RedirectToAction("ListMyErrand");
            }
            User helperUser = await _userManager.FindByIdAsync(errand.HelperUserId);

            errand.Done = true;
            helperUser.CompletedErrands += 1;

            await _userManager.UpdateAsync(helperUser);
            await _errandRepository.UpdateAsync(errand);
            return RedirectToAction("ListMyErrand");
        }
    }
}
