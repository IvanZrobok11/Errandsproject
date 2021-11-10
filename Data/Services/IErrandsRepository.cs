using Errands.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Errands.Data.Services
{
    public interface IErrandsRepository
    {
        IEnumerable<Errand> Errands { get; }
        IEnumerable<Errand> Search(string searchText, string userid);
        IEnumerable<Errand> GetErrandsByUserId(string id);
        IEnumerable<Errand> GetErrandsByHelperUserId(string id);
        Task<Errand> GetErrandByIdAsync(Guid id);
        Task CreateErrandAsync(Errand errand, IEnumerable<FileModel> files);
        Task UpdateAsync(Errand errand);
        Task DeleteAsync(Guid id);
        Task<FileModel> GetFileByIdAsync(Guid id);
    }
}
