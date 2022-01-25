using Errands.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Errands.Data.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ErrandsDbContext _context;



        public UsersRepository(ErrandsDbContext context)
        {
            _context = context;
        }
        public IQueryable<User> Users => _context.Users;

        public IQueryable<BlockedUser> BlockedUsers => _context.BlockedUsers;
        public async Task<Logo> GetLogoAsync(string userId)
        {
            var logo = await _context.Logos.FirstOrDefaultAsync(l => l.UserId == userId);
            return logo;
        }
        public async Task<string> GetLogoPathAsync(string userId)
        {
            IEnumerable<Logo> list = await _context.Logos.Where(f => f.UserId == userId).ToListAsync();
            string path = list.Select(p => p.Path).FirstOrDefault();
            return path;
        }
        public async Task AddLogoAsync(Logo logo)
        {
            await _context.Logos.AddAsync(logo);
            await _context.SaveChangesAsync();
        }
        public async Task<Logo> DeleteLogoAsync(string userid)
        {
            var logo = await GetLogoAsync(userid);
            if (logo == null)
            {
                return null;
            }
            var logoEntry = _context.Logos.Remove(logo).Entity;
            return logoEntry;
        }
        public async Task<User> GetUserInfoAsync(string userId)
        {
            var user = await _context.Users.Include(l => l.Logo)
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task AddBlockedUsersByEmail(string email)
        {
            await _context.BlockedUsers.AddAsync(new BlockedUser { Email = email });
        }
    }
}
