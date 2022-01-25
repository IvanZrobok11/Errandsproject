using Errands.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Errands.Application.Common.Services;

namespace Errands.Data.Services
{
    public interface IErrandsRepository
    {
        IEnumerable<Errand> Search(string searchText, string userid);
        Task<Errand> GetErrandByIdAsync(Guid id);
        Task CreateErrandAsync(Errand errand, IEnumerable<FileModel> files);
        Task UpdateAsync(Errand errand);
        Task DeleteAsync(Guid id);
        IQueryable<Errand> Errands { get; }

        //Task<FileModel> GetFileByIdAsync(Guid id);

    }
}
