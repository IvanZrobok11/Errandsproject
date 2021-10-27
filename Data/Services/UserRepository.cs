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
        private readonly LogoImageProfile _logoImageProfile;
        private readonly IWebHostEnvironment _appEnvironment;
        public UserRepository(ErrandsDbContext context, LogoImageProfile logoImageProfile, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _logoImageProfile = logoImageProfile;
            _appEnvironment = appEnvironment;

        }
        public async Task<FileModel> GetLogoAsync(string userId)
        {
            var logo = await _context.FileModels.FirstOrDefaultAsync(l => l.UserId == userId);
            return logo;
        }
        public string GetLogoPathAsync(string userId)
        {
            var path = _context.FileModels.Where(f => f.UserId == userId)
                .AsEnumerable()
                .Select(p => p.Path)
                .FirstOrDefault();
            return path;
        }
        public async Task AddLogoAsync(FileModel file)
        {
            await _context.FileModels.AddAsync(file);
            await _context.SaveChangesAsync();
        }


    }
}
