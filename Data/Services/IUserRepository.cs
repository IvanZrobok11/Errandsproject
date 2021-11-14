using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Errands.Domain.Models;

namespace Errands.Data.Services
{
    public interface IUserRepository
    {
        Task<Logo> GetLogoAsync(string userId);
        Task<string> GetLogoPathAsync(string userId);
        Task AddLogoAsync(Logo logo);
        Logo DeleteLogo(Logo logo);
        Task<Logo> DeleteLogoAsync(string userid);
        Task<User> GetUserInfoAsync(string userId);
        Task AddEmailToBlocked(string email);
        List<BlockedUser> ListBlockedUsers { get; }
    }
    
}
