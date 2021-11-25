using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Application.Exceptions
{
    public class WrongExtensionFileException : Exception
    {
        public WrongExtensionFileException(string extension) : base($"File has unacceptable extension \"{extension}\"")
        {
            
        }
    }
}
