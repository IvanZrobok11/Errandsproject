using AutoMapper;
using Errands.Application.Common.Services;
using Errands.Application.Exceptions;
using Errands.Data.Services;
using Errands.Domain.Models;
using Errands.Mvc.Extensions;
using Errands.Mvc.Models.ViewModels;
using Errands.Mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;

namespace Errands.Mvc.Controllers
{
    [Authorize]
    public class ErrandController : Controller
    {
        private readonly IErrandsService _errandService;
        private readonly IFileServices _fileServices;
        private readonly UserManager<User> _userManager;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;
        private readonly ILogger<ErrandController> _logger;
        public readonly IMessagesService _messagesService;

        public ErrandController(
            IErrandsService errandService, IFileServices fileServices,
            UserManager<User> userManager, IDateTimeProvider dateTimeProvider,
            IMapper mapper, ILogger<ErrandController> logger,
            IMessagesService messagesService)
        {
            _errandService = errandService;
            _fileServices = fileServices;
            _userManager = userManager;
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
            _logger = logger;
            _messagesService = messagesService;
        }
        //

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetErrand(Guid errandId)
        {
            Errand errand = await _errandService.GetErrandByIdAsync(errandId);
            if (errand == null)
            {
                return NotFound();
            }
            return View(errand);
        }
        [HttpGet]
        public async Task<IActionResult> ListMyErrand([FromQuery] int pageNumber = 1)
        {
            var userId = User.GetId();
            var errands = await _errandService
                .GetErrandsByUserIdAsync(userId, pageNumber,
                    ControllerConstants.ItemPerMyErrandsPage);
            return View(new ListMyErrandsViewModel
            {
                Errands = errands,
                PageInfo = new PageInfo
                {
                    ItemsPerPage = ControllerConstants.ItemPerMyErrandsPage,
                    TotalItems = await _errandService.MyErrandsTotal(userId),
                    CurrentPage = pageNumber
                }
            });
        }
        [HttpGet]
        public async Task<IActionResult> ListErrandToDo([FromQuery] int pageNumber = 1)
        {
            var userId = User.GetId();
            var errands = await _errandService
                .GetErrandsByHelperUserIdAsync(userId, pageNumber,
                    ControllerConstants.ItemPerToDoErrandsPage);
            return View(new ListErrandsToDoViewModel()
            {
                Errands = errands,
                PageInfo = new PageInfo()
                {
                    ItemsPerPage = ControllerConstants.ItemPerToDoErrandsPage,
                    TotalItems = await _errandService.ErrandsToDoTotal(userId),
                    CurrentPage = pageNumber
                }
            });
        }


        [HttpGet]
        public async Task<VirtualFileResult> GetFile(Guid id)
        {
            var file = await _errandService.GetFileByIdAsync(id);
            return File($"{file.Path}", "AppContext/pdf", file.Name);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateErrandModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateErrandModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var errand = _mapper.Map<Errand>(model);
            {
                errand.CreationDate = _dateTimeProvider.Now();
                errand.UserId = this.User.GetId();
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
                        TempData[TempDataResult.ErrorMessage] = "File have unallowable extension";
                        return View(model);
                    }
                    catch (InvalidSizeFileException)
                    {
                        TempData[TempDataResult.ErrorMessage] = "File size is bigger than allowed";
                        return View(model);
                    }
                    catch (ImageProcessingException)
                    {
                        TempData[TempDataResult.ErrorMessage] = "Processing exception";
                        return View(model);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            _logger.LogInformation("Created new errand:  <{title}>.", model.Title);
            await _errandService.CreateErrandAsync(errand, files);
            TempData[TempDataResult.SuccessMessage] = "Errand success created";
            return RedirectToAction(nameof(this.ListMyErrand));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var errand = await _errandService.GetErrandByIdAsync(id);
            if (errand == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EditErrandModel>(errand);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditErrandModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var map = _mapper.Map<Errand>(model);
            var errand = await _errandService.GetErrandByIdAsync(model.Id);
            {
                errand.Cost = map.Cost;
                errand.Description = map.Description;
                errand.Title = map.Title;
            }
            await _errandService.UpdateAsync(errand);
            TempData[TempDataResult.SuccessMessage] = "Errand success edited";
            return RedirectToAction(nameof(ListMyErrand));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var errand = await _errandService.GetErrandByIdAsync(id);
            if (errand == null)
            {
                return NotFound();
            }
            try
            {
                var paths = errand.FileModels.Select(p => p.Path);
                foreach (var p in paths)
                {
                    _fileServices.DeleteFile(p);
                }
                await _errandService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error delete file");
                throw new NotFoundException(errand.Title, id);
            }
            _logger.LogInformation("Deleted errand:  <{title}>.", errand.Title);
            return RedirectToAction(nameof(ListMyErrand));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Take(Guid id)
        {
            var errand = await _errandService.GetErrandByIdAsync(id);
            if (!errand.Active)
            {
                return RedirectToAction("Index", "Home");
            }
            errand.Active = false;
            errand.HelperUserId = User.GetId();

            if (await _messagesService
                    .GetChatByUsersIdAsync(errand.HelperUserId, errand.UserId) == null)
            {
                await _messagesService.CreateChatAsync(errand.UserId, errand.HelperUserId);
            }
            await _errandService.UpdateAsync(errand);
            TempData[TempDataResult.SuccessMessage] = $"You take <{errand.Title}> errand now!";
            return RedirectToAction("ListErrandToDo");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var errand = await _errandService.GetErrandByIdAsync(id);
            if (errand.Done)
            {
                TempData[TempDataResult.ErrorMessage] = "This errand already confirmed!";
                return RedirectToAction("ListMyErrand");
            }
            User helperUser = await _userManager.FindByIdAsync(errand.HelperUserId);

            errand.Done = true;
            helperUser.CompletedErrands += 1;

            await _userManager.UpdateAsync(helperUser);
            await _errandService.UpdateAsync(errand);

            return RedirectToAction("ListMyErrand");
        }
    }
}
