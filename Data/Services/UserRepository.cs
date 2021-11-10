using Errands.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.IO;
using Errrands.Application.Common;
using System.Linq;
using SixLabors.ImageSharp;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp.Processing;

namespace Errands.Data.Services
{
    public class UserRepository
    {
        private readonly ErrandsDbContext _context;
        public UserRepository(ErrandsDbContext context)
        {
            _context = context;
        }
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
        public Logo DeleteLogo(Logo logo)
        {
            var logoEntry = _context.Logos.Remove(logo).Entity;
            return logoEntry;
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
        public async Task AddEmailToBlocked(string email)
        {
            await _context.BlockedUsers.AddAsync(new BlockedUser { Email = email });
        }
        public List<BlockedUser> ListBlockedUsers =>
            _context.BlockedUsers.ToList();
    }
}
