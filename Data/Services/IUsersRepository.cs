using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Errands.Domain.Models;

namespace Errands.Data.Services
{
    public interface IUsersRepository
    {
        Task<Logo> GetLogoAsync(string userId);
        Task<string> GetLogoPathAsync(string userId);
        Task AddLogoAsync(Logo logo);
        Task<Logo> DeleteLogoAsync(string userid);
        Task<User> GetUserInfoAsync(string userId);
        IQueryable<User> Users { get; }
        IQueryable<BlockedUser> BlockedUsers { get; }
        Task AddBlockedUsersByEmail(string email);
    }
    
}
