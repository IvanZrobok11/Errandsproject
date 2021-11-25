using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Application.Exceptions
{
    public class InvalidSizeFileException : Exception 
    {
        public InvalidSizeFileException(string errorMessage) : 
            base(errorMessage)
        {
                
        }
    }
}
