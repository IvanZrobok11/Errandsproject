using Errands.Data.Services;
using Errands.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Errands.Data.Services
{
    public class ErrandsRepository : IErrandsRepository
    {
        private ErrandsDbContext _context;
        public ErrandsRepository(ErrandsDbContext context)
        {
            _context = context;

        }
        public IEnumerable<Errand> Errands => _context.Errands.Include(f => f.FileModels);
        public Task<Errand> GetErrandByIdAsync(Guid id)
        {
            var errand = _context.Errands.Include(f => f.FileModels).Where(e => e.Id == id).SingleOrDefaultAsync();
            return errand;
        }
        public async Task DeleteAsync(Guid id)
        {
            var errand = await GetErrandByIdAsync(id); 
            
            _context.Errands.Remove(errand);
            await _context.SaveChangesAsync();
        }
        public IEnumerable<Errand> GetErrandsByUserId(string id)
        {
            var errands = _context.Errands.Where(i => i.UserId == id).ToList();
            return errands;
        }
        public IEnumerable<Errand> Search(string searchText, string userid)
        {
            IEnumerable<Errand> result = Enumerable.Empty<Errand>();

            if (!string.IsNullOrEmpty(searchText))
            {
                result = _context.Errands.Where(n => n.Title.Contains(searchText) && n.UserId == userid);
            }

            return result;
        }
        public async Task UpdateAsync(Errand errand)
        {
            /*var err = _context.Errands.FirstOrDefault(e => e.Id == errand.Id);
            Errand entry = err;
            entry.Price = errand.Price;*/
            _context.Update(errand);
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
        public async Task AttachFileAsync(FileModel file)
        {
            await _context.FileModels.AddAsync(file);
            await _context.SaveChangesAsync();
        }

        public async Task<FileModel> GetFileByIdAsync(Guid id)
        {
            var file = await _context.FileModels.Where(e => e.Id == id).SingleOrDefaultAsync();
            return file;
        }

    }
}