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
    public class ErrandsService : IErrandsService
    {
        private readonly ErrandsDbContext _context;
        private readonly IMapper _mapper;
        public ErrandsService(ErrandsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        public async Task<IEnumerable<Errand>> GetUnfinishedErrands(string userId)
            => await _context.Errands.Where(e => e.UserId == userId)
                .Where(e => !e.Done && e.HelperUserId != null).ToListAsync();

        public async Task<IEnumerable<GetMyErrandServiceModel>> GetErrandsByUserIdAsync(string id, int currentPage, int pageSize)
        {
            var errands = _context.Errands
                .Where(i => i.UserId == id)
                .AsQueryable();
            return await SelectWith<GetMyErrandServiceModel>(errands, currentPage, pageSize);
        }
        public async Task<IEnumerable<GetErrandToDoServiceModel>> GetErrandsByHelperUserIdAsync(string id, int currentPage, int pageSize)
        {
            var errands = _context.Errands
                .Where(i => i.HelperUserId == id)
                .AsQueryable();
            return await SelectWith<GetErrandToDoServiceModel>(errands, currentPage, pageSize);
        }

        public async Task<IEnumerable<ListErrandsServiceModel>> AllAsync(int currentPage, int pageSize)
        {
            var errand =  _context.Errands
                .Include(f => f.FileModels)
                .Include(u => u.User).AsQueryable();
            return await SelectWith<ListErrandsServiceModel>(errand, currentPage, pageSize);
        }
        private async Task<IEnumerable<TModel>> SelectWith<TModel>(IQueryable<Errand> query, int currentPage, int pageSize) 
            where TModel : class
        {
            return await query
                .OrderByDescending(a => a.CreationDate)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }


        
        public async Task<int> ErrandsToDoTotal(string userId)
            => await _context.Errands
                .Where(i => i.HelperUserId == userId)
                .CountAsync();
        public async Task<int> MyErrandsTotal(string userId)
            => await _context.Errands
                .Where(i => i.UserId == userId)
                .CountAsync();
        public async Task<int> Total() =>
            await _context.Errands.CountAsync();
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