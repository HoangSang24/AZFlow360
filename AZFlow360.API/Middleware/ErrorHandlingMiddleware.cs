using System.Net;
using System.Text.Json;
using FluentValidation; // <--- THÊM DÒNG NÀY

namespace AZFlow360.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            object? errors = null;

            switch (exception)
            {
                case ValidationException validationException: // Bây giờ sẽ được nhận diện chính xác
                    statusCode = HttpStatusCode.BadRequest;
                    errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                    break;
                default:
                    _logger.LogError(exception, "An unhandled exception has occurred.");
                    errors = new { error = "An unexpected error occurred on the server." };
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new { errors });
            await context.Response.WriteAsync(result);
        }
    }
}