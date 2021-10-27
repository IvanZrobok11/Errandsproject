using System;
using System.Collections.Generic;
using System.Text;

namespace Errrands.Application.Common
{
    public class LogoImageProfile : IImageProfile
    {
        public LogoImageProfile()
        {
            AllowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
        }
        private const int mb = 1048576;
        public int Width => 300;

        public int Height => 300;

        public int MaxSizeBytes => 5 * mb;

        public string Folder => "logos";

        public IEnumerable<string> AllowedExtensions { get; }
    }
}
