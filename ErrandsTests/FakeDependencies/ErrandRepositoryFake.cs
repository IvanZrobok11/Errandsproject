using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Errands.Data.Services;
using Errands.Domain.Models;

namespace ErrandsTests.FakeDependencies
{
    public class ErrandRepositoryFake : IErrandsRepository
    {
        //private ErrandsTestData testData;
        public IEnumerable<Errand> Errands => ErrandsTestData.GetErrands(10);

        public IEnumerable<Errand> Search(string searchText, string userid)
        {
            return null;
        }

        public IEnumerable<Errand> GetErrandsByUserId(string id)
        {
            return ErrandsTestData.GetErrands(1);
        }

        public IEnumerable<Errand> GetErrandsByHelperUserId(string id)
        {
            return ErrandsTestData.GetErrands(1);
        }

        public async Task<Errand> GetErrandByIdAsync(Guid id)
        {
            return ErrandsTestData.GetErrands(1).SingleOrDefault();
        }

        public async Task CreateErrandAsync(Errand errand, IEnumerable<FileModel> files)
        {
            //return Task.CompletedTask;
        }

        public async Task UpdateAsync(Errand errand)
        {
            //return _errandsRepositoryImplementation.UpdateAsync(errand);
        }

        public async Task DeleteAsync(Guid id)
        {
            //return _errandsRepositoryImplementation.DeleteAsync(id);
        }

        public Task<FileModel> GetFileByIdAsync(Guid id)
        {
            return null;
        }
    }
}
