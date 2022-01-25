using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Errands.Domain.Models;

namespace BusinessLogic.Interfaces
{
    public interface IUsersService
    {
        Task<Logo> GetLogoAsync(string userId);
        Task<string> GetLogoPathAsync(string userId);
        Task AddLogoAsync(Logo logo);
        Task<Logo> DeleteLogoAsync(string userid);
        Task<User> GetUserInfoAsync(string userId);
        Task AddBlockedUsersByEmail(string email);
        List<BlockedUser> ListBlockedUsers { get; }
    }
}
