using System.Net;
using Tutorial12.API.DTOs;

namespace Tutorial12.API.Helpers;

/// <summary>
/// Example of usage custom middleware, that intercepts requests and act if there any errors during request execution
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
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
        _logger.LogError(exception, "An unexpected error occurred.");

        var response = exception switch
        {
            ApplicationException _ => new ExceptionResponseDto(HttpStatusCode.BadRequest, "Application exception occurred."),
            KeyNotFoundException _ => new ExceptionResponseDto(HttpStatusCode.NotFound, "The request endpoint not found."),
            UnauthorizedAccessException _ => new ExceptionResponseDto(HttpStatusCode.Unauthorized, "Unauthorized."),
            _ => new ExceptionResponseDto(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}