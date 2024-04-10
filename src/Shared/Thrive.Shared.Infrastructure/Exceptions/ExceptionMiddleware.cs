using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Thrive.Shared.Abstractions.Exceptions;

namespace Thrive.Shared.Infrastructure.Exceptions;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new ExceptionResponse
        {
            Title = exception.Message,
            Source = exception.TargetSite?.DeclaringType?.Namespace ?? string.Empty
        };

        if (exception is BaseException baseException)
        {
            response.Code = baseException.Code ?? response.Code;
            response.StatusCode = baseException.StatusCode;
            response.Errors.AddRange(baseException.Errors);
        }
        else
        {
            response.Title = "Internal server error.";
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        if (!_environment.IsDevelopment())
        {
            response.Source = string.Empty;
        }
        
        context.Response.StatusCode = (int)response.StatusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
