using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Errands.Domain.Models;
using Errands.Mvc.Services;
using Microsoft.AspNetCore.Http;

namespace ErrandsTests.FakeDependencies
{
    public class FakeFileServices : IFileServices
    {
        public void DeleteFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<Logo> SaveLogoAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<FileModel> SaveFile(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
