using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Errands.Mvc.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now();
        DateTime UtcNow();
    }
}
