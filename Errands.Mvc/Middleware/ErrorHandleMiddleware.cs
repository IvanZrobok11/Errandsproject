using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;

namespace Errands.Mvc.Middleware
{
    public class ErrorHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                Logging(e);
                var code = HttpStatusCode.InternalServerError;
                var errorMessage = e.Message;

                var result = JsonConvert.SerializeObject(new { Code = code.ToString(), ErrorMessage = errorMessage });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;

                await context.Response.WriteAsync(result);
            }
        }

        private void Logging(Exception e)
        {
            Log.Logger.ForContext("log", "EXCEPTION")
                .Error("Middleware error: {message}\n Stack trace: {stack_trace}",
                    e.Message, e.StackTrace);
        }
    }
}
