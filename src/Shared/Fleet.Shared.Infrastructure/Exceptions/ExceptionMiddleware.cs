using System.Net;
using Fleet.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Fleet.Shared.Infrastructure.Exceptions;

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
        var response = new ExceptionResponse()
        {
            Title = exception.Message,
        };

        if (exception is BaseException baseException)
        {
            response.StatusCode = baseException.StatusCode;

            foreach (var message in baseException.Errors)
            {
                response.Errors.Add(message);
            }
        }

        response.Source = exception.TargetSite?.DeclaringType?.Namespace ?? string.Empty;

        if (response.StatusCode is HttpStatusCode.InternalServerError && !_environment.IsDevelopment())
        {
            response.Title = "Internal server error";
        }

        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}