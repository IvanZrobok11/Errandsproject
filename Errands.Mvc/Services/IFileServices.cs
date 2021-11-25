using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Errands.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Errands.Mvc.Services
{
    public interface IFileServices
    {
        void DeleteFile(string filePath);
        Logo SaveLogo(IFormFile file);
    }
}
