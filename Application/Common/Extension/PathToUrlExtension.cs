using System;
using System.Collections.Generic;
using System.Text;

namespace Errrands.Application.Common.Extension
{
    public static class PathToUrlExtension
    {
        public static string PathToUrl(this string path)
        {
            return path?.Replace("\\", "/");
        }
    }
}
