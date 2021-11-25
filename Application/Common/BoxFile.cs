
using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Application.Common
{
    public class BoxFile : IFileProfile
    {
        private const int mb = 1048576;
        public BoxFile()
        {
            AllowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".docx", ".doc",".pdf", ".txt", ".png"};
        }
        public int MaxSizeBytes => 10 * mb;

        public string Folder => "workingFiles"; 

        public IEnumerable<string> AllowedExtensions { get; }

        //string IFileProfile.Folder => throw new NotImplementedException();
    }
}
