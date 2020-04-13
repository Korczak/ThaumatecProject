using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Log = Serilog.Log;


namespace Thaumatec.Core.Logging
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Error in {context.Request.Path}");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
