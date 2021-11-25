
using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Application.Common
{
    public interface IImageProfile : IFileProfile
    {
        int Width { get; }
        int Height { get; }
    }
}
