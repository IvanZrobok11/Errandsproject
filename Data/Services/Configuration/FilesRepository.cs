using Errands.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Errands.Data.Services.Configuration
{
    public class FilesRepository : IFilesRepository
    {
        private ErrandsDbContext _dbContext;
        public FilesRepository(ErrandsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<FileModel> Files => _dbContext.FileModels;

        public async Task AttachFileAsync(FileModel file)
        {
            await _dbContext.FileModels.AddAsync(file);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<FileModel> GetFileByIdAsync(Guid id)
        {
            var file = await _dbContext
                .FileModels.Where(e => e.Id == id).SingleOrDefaultAsync();
            return file;
        }
    }
}
