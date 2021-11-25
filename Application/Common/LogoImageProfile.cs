using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Application.Common
{
    public class LogoImageProfile : IImageProfile
    {
        public LogoImageProfile()
        {
            AllowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".jfif", ".jp2", ".bmp"};
        }
        private const int mb = 1048576;
        public int Width => 150;

        public int Height => 150;

        public int MaxSizeBytes => 5 * mb;

        public string Folder => "logos";

        public IEnumerable<string> AllowedExtensions { get; }
    }
}
