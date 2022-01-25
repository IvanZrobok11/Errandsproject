using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Errands.Application.Common.Services;
using Errands.Domain.Models;

namespace BusinessLogic.Interfaces
{
    public interface IErrandsService
    {
        IEnumerable<Errand> Search(string searchText, string userid);
        Task<Errand> GetErrandByIdAsync(Guid id);
        Task CreateErrandAsync(Errand errand, IEnumerable<FileModel> files);
        Task UpdateAsync(Errand errand);
        Task DeleteAsync(Guid id);
        Task<FileModel> GetFileByIdAsync(Guid id);

        Task<IEnumerable<ListErrandsServiceModel>> AllAsync(int currentPage, int pageSize);
        Task<IEnumerable<GetErrandToDoServiceModel>> GetErrandsByHelperUserIdAsync(string id, int currentPage, int pageSize);
        Task<IEnumerable<GetMyErrandServiceModel>> GetErrandsByUserIdAsync(string id, int currentPage, int pageSize);
        Task<IEnumerable<Errand>> GetUnfinishedErrands(string userId);
        Task<int> Total();
        Task<int> MyErrandsTotal(string userId);
        Task<int> ErrandsToDoTotal(string userId);
    }
}
