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
        Errand GetErrandById(Guid id);
        IEnumerable<Errand> Search(string searchText, string userid);
        IEnumerable<Errand> GetErrandsByUserId(string id);
        Task CreateErrandAsync(Errand errand);
        Task UpdateAsync(Errand errand);
        Task DeleteAsync(Guid id);
    }
}
