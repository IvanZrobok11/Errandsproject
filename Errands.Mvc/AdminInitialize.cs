using Errands.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Errands.Mvc
{
    public class AdminInitialize
    {
        private static List<AdminModel> admins;
        private static void InitData()
        {
            admins = new List<AdminModel>();
            admins.Add(new AdminModel
            {
                Email = "AdminErrandMain190215@gmail.com",
                Password = "Qwmn12pozx09aslk",
                UserName = "AdminAdmin"
            });
            admins.Add(new AdminModel
            {
                Email = "AdminMainModerator@gmail.com",
                Password = "Qwmn12pozx09aslk123",
                UserName = "Moderator"
            });
        }
        public static async Task InitializeAsync(UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Administrator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }
            InitData();
            foreach (var admin in admins)
            {
                if (await userManager.FindByEmailAsync(admin.Email) == null)
                {
                    User user = new User { Email = admin.Email, UserName = admin.UserName };
                    IdentityResult result = await userManager.CreateAsync(user, admin.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Administrator");
                    }
                }
            }
        }
        
    }
    internal class AdminModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
}
