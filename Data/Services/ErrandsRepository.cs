using AutoMapper;
using AutoMapper.QueryableExtensions;
using Errands.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Errands.Application.Common.Services;

namespace Errands.Data.Services
{
    public class ErrandsRepository : IErrandsRepository
    {
        private readonly ErrandsDbContext _context;
        private readonly IMapper _mapper;

        public IQueryable<Errand> Errands => _context.Errands;

        public ErrandsRepository(ErrandsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<Errand> GetErrandByIdAsync(Guid id)
        {
            var errand = _context.Errands
                .Include(f => f.FileModels)
                .Include(u => u.User)
                .Where(e => e.Id == id).SingleOrDefaultAsync();
            return errand;
        }
        public async Task DeleteAsync(Guid id)
        {
            var errand = await GetErrandByIdAsync(id);

            _context.Errands.Remove(errand);
            await _context.SaveChangesAsync();
        }
        public IEnumerable<Errand> Search(string searchText, string userid)
        {
            IEnumerable<Errand> result = Enumerable.Empty<Errand>();

            if (!string.IsNullOrEmpty(searchText))
            {
                result = _context.Errands
                    .Where(n => n.Title
                        .Contains(searchText) && n.UserId == userid);
            }

            return result;
        }
        public async Task UpdateAsync(Errand changeErrand)
        {
            _context.Update(changeErrand);
            await _context.SaveChangesAsync();
        }
        public async Task CreateErrandAsync(Errand errand, IEnumerable<FileModel> files)
        {
            await _context.Errands.AddAsync(errand);
            if (files != null)
            {
                await _context.AddRangeAsync(files);
            }
            await _context.SaveChangesAsync();
        }
        //public async Task AttachFileAsync(FileModel file)
        //{
        //    await _context.FileModels.AddAsync(file);
        //    await _context.SaveChangesAsync();
        //}
        //public async Task<FileModel> GetFileByIdAsync(Guid id)
        //{
        //    var file = await _context.FileModels.Where(e => e.Id == id).SingleOrDefaultAsync();
        //    return file;
        //}

    }
}