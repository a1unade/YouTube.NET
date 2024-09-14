using System.Net;
using System.Text.Json;
using FluentValidation;

namespace YouTube.WebAPI.Middleware;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = String.Empty;

        switch (exception)
        {
            case ValidationException validationException :
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == String.Empty)
        {
            result = JsonSerializer.Serialize(new { Error = exception.Message });
        }
        
        return context.Response.WriteAsync(result);
    }
}