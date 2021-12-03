﻿using Errands.Data.Services;
using Errands.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Errands.Mvc.Controllers
{
    [Authorize(Roles= "Administrator")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IUserService userService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult ListBlockedEmails()
        {           
            return View(_userService.ListBlockedUsers);
        }
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            var exceptUsers = await _userManager.GetUsersInRoleAsync("Administrator");
            return View(_userManager.Users.AsEnumerable().Except(exceptUsers));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAndBlockedUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                await _userService.AddEmailToBlocked(user.Email);
            }
            return RedirectToAction("ListUsers");
        }
    }
}
