using Errands.Data.Services;
using Errands.Domain.Models;
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
        public IEnumerable<Errand> Errands => _context.Errands;

        public async Task CreateErrandAsync(Errand errand)
        {
            await _context.Errands.AddAsync(errand);
            await _context.SaveChangesAsync();
        }

        public Errand GetErrandById(Guid id)
        {
            var errand = _context.Errands.Where(e => e.Id == id).SingleOrDefault();
            return errand;
        }
        public async Task DeleteAsync(Guid id)
        {
            var errand = GetErrandById(id); 
            
            _context.Errands.Remove(errand);
            await _context.SaveChangesAsync();
        }


        public IEnumerable<Errand> GetErrandsByUserId(string id)
        {
            var errands = _context.Errands.Where(i => i.UserId == id).ToList();
            return errands;
        }

        public async Task UpdateAsync(Errand errand)
        {
            /*var err = _context.Errands.FirstOrDefault(e => e.Id == errand.Id);
            Errand entry = err;
            entry.Price = errand.Price;*/
            _context.Update(errand);
            await _context.SaveChangesAsync();
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

    }
}