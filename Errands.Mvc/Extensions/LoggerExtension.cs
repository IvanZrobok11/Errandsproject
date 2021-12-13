using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Errands.Mvc.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Errands.Mvc.Extensions
{
    public static class LoggerExtension
    {
        public static IApplicationBuilder UseErrorHandle(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandleMiddleware>();
        }
    }
}
