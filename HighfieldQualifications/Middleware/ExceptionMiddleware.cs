namespace HighfieldQualifications.Middleware
{
    using log4net;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;

    public class ErrorLoggingMiddleware
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ErrorLoggingMiddleware));
        private readonly RequestDelegate nextRequest;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            nextRequest = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await nextRequest(context);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat($"Error: {ex}");

                throw;
            }
        }
    }
}
