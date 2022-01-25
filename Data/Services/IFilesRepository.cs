using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Errands.Domain.Models;

namespace Errands.Data.Services
{
    public interface IFilesRepository
    {
        Task AttachFileAsync(FileModel file);
        Task<FileModel> GetFileByIdAsync(Guid id);
        IQueryable<FileModel> Files { get; }
    }
}
