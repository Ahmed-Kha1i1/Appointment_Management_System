using AMS.Application.Common.Response;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AMS.Application.Common.Exceptions_Handlers
{
    public class GlobalExceptionHandler : ResponseHandler, IExceptionHandler
    {
        private readonly ILogger _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogCritical(exception, "An unhandled exception occurred: {Message}", exception.Message);

            switch (exception)
            {
                case UnauthorizedAccessException unauthorizedAccessException:
                    await HandleUnauthorizedAccessExceptionAsync(httpContext, unauthorizedAccessException);
                    break;
                default:
                    await HandleUnknownExceptionAsync(httpContext, exception);
                    break;
            }

            return true;
        }
        private async Task HandleUnauthorizedAccessExceptionAsync(HttpContext httpContext, UnauthorizedAccessException exception)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await httpContext.Response.WriteAsJsonAsync(Unauthorized<string>("Unauthorized Access"));
        }


        private async Task HandleUnknownExceptionAsync(HttpContext httpContext, Exception exception)
        {
            
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(Fail(exception.Data, "An operation failed"));
        }
    }
}
