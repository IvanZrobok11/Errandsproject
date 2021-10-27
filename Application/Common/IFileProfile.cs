using System;
using System.Collections.Generic;
using System.Text;

namespace Errrands.Application.Common
{
    public interface IFileProfile
    {
        int MaxSizeBytes { get; }
        string Folder { get; }
        IEnumerable<string> AllowedExtensions { get; }
    }
}
