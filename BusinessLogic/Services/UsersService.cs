using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Errands.Data.Services;
using Errands.Domain.Models;

namespace BusinessLogic.Services
{
    public class UsersService : IUsersService
    {
        public UsersService(IUsersRepository repo)
        {
            _usersRepository = repo;    
        }

        private readonly IUsersRepository _usersRepository;
        public async Task<Logo> GetLogoAsync(string userId)
        {
            return await _usersRepository.GetLogoAsync(userId);
        }
        public async Task<string> GetLogoPathAsync(string userId)
        {
            return await GetLogoPathAsync(userId);
        }
        public async Task AddLogoAsync(Logo logo)
        {
            await _usersRepository.AddLogoAsync(logo);
        }
        public async Task<Logo> DeleteLogoAsync(string userid)
        {
            return await _usersRepository.DeleteLogoAsync(userid);
        }
        public async Task<User> GetUserInfoAsync(string userId)
        {
            return await GetUserInfoAsync(userId);
        }
        public async Task AddBlockedUsersByEmail(string email)
        {
            await _usersRepository.AddBlockedUsersByEmail(email);
        }
        public List<BlockedUser> ListBlockedUsers =>
            _usersRepository.BlockedUsers.ToList();
    }
}
