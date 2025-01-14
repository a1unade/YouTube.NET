using Microsoft.AspNetCore.Builder;
using YouTube.WebAPI.Middleware;

namespace YouTube.WebAPI.Configurations;

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}