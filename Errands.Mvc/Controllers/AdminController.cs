using Errands.Data.Services;
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
        private readonly IUsersRepository _usersRepository;
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IUsersRepository usersRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _usersRepository = usersRepository;
        }
        [HttpGet]
        public IActionResult ListBlockedEmails()
        {           
            return View(_usersRepository.ListBlockedUsers);
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
                await _usersRepository.AddEmailToBlocked(user.Email);
            }
            return RedirectToAction("ListUsers");
        }
    }
}
