using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Interfaces;
using Errands.Application.Common.Services;
using Errands.Data.Services;
using Errands.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class ErrandsService : Interfaces.IErrandsService
    {
        private readonly ErrandsRepository _errandsRepository;
        private readonly IMapper _mapper;
        private IFilesRepository _filesRepository;
        public ErrandsService(ErrandsRepository errandsRepository, IMapper mapper, IFilesRepository filesRepository)
        {
            _errandsRepository = errandsRepository;
            _mapper = mapper;
            _filesRepository = filesRepository;
        }
        public async Task<IEnumerable<Errand>> GetUnfinishedErrands(string userId)
            => await _errandsRepository.Errands.Where(e => e.UserId == userId)
                .Where(e => !e.Done && e.HelperUserId != null).ToListAsync();

        public async Task<IEnumerable<GetMyErrandServiceModel>> GetErrandsByUserIdAsync(string id, int currentPage, int pageSize)
        {
            var errands = _errandsRepository.Errands
                .Where(i => i.UserId == id)
                .AsQueryable();
            return await SelectWith<GetMyErrandServiceModel>(errands, currentPage, pageSize);
        }
        public async Task<IEnumerable<GetErrandToDoServiceModel>> GetErrandsByHelperUserIdAsync(string id, int currentPage, int pageSize)
        {
            var errands = _errandsRepository.Errands
                .Where(i => i.HelperUserId == id)
                .AsQueryable();
            return await SelectWith<GetErrandToDoServiceModel>(errands, currentPage, pageSize);
        }

        public async Task<IEnumerable<ListErrandsServiceModel>> AllAsync(int currentPage, int pageSize)
        {
            var errand = _errandsRepository.Errands
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
            => await _errandsRepository.Errands
                .Where(i => i.HelperUserId == userId)
                .CountAsync();
        public async Task<int> MyErrandsTotal(string userId)
            => await _errandsRepository.Errands
                .Where(i => i.UserId == userId)
                .CountAsync();
        public async Task<int> Total() =>
            await _errandsRepository.Errands.CountAsync();
        public Task<Errand> GetErrandByIdAsync(Guid id)
        {
            var errand = _errandsRepository.Errands
                .Include(f => f.FileModels)
                .Include(u => u.User)
                .Where(e => e.Id == id).SingleOrDefaultAsync();
            return errand;
        }
        public async Task DeleteAsync(Guid id)
        {
            await _errandsRepository.DeleteAsync(id);
        }
        public IEnumerable<Errand> Search(string searchText, string userid)
        {
            return _errandsRepository.Search(searchText, userid);
        }
        public async Task UpdateAsync(Errand changeErrand)
        {
            await _errandsRepository.UpdateAsync(changeErrand);
        }
        public async Task CreateErrandAsync(Errand errand, IEnumerable<FileModel> files)
        {
            await _errandsRepository.CreateErrandAsync(errand, files);
        }
        public async Task AttachFileAsync(FileModel file)
        {
            await _filesRepository.AttachFileAsync(file);
        }
        public async Task<FileModel> GetFileByIdAsync(Guid id)
        {
            return await _filesRepository.GetFileByIdAsync(id);
        }
    }
}
