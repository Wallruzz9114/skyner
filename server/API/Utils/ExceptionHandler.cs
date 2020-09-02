using System;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Utils
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionHandler> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionHandler(
            RequestDelegate requestDelegate,
            ILogger<ExceptionHandler> logger,
            IHostEnvironment hostEnvironment)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = _hostEnvironment.IsDevelopment()
                    ? new APIException(
                        StatusCodes.Status500InternalServerError,
                        exception.Message,
                        exception.StackTrace.ToString()
                    )
                    : new APIResponse(StatusCodes.Status500InternalServerError);
                var serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, serializerOptions);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}