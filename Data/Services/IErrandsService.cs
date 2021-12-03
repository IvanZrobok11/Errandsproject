﻿using Errands.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Errands.Application.Common.Services;

namespace Errands.Data.Services
{
    public interface IErrandsService 
    {
        Task<IEnumerable<ListErrandsServiceModel>> AllAsync(int currentPage, int pageSize);
        Task<IEnumerable<GetErrandToDoServiceModel>> GetErrandsByHelperUserIdAsync(string id, int currentPage, int pageSize);
        Task<IEnumerable<GetMyErrandServiceModel>> GetErrandsByUserIdAsync(string id, int currentPage, int pageSize);
        Task<int> Total();
        Task<int> MyErrandsTotal(string id);
        Task<int> ErrandsToDoTotal(string id);

        IEnumerable<Errand> Search(string searchText, string userid);
        Task<Errand> GetErrandByIdAsync(Guid id);
        Task CreateErrandAsync(Errand errand, IEnumerable<FileModel> files);
        Task UpdateAsync(Errand errand);
        Task DeleteAsync(Guid id);
        Task<FileModel> GetFileByIdAsync(Guid id);
    }
}
