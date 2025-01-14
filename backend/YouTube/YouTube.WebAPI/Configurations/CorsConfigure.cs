using Microsoft.AspNetCore.Builder;

namespace YouTube.WebAPI.Configurations;

public static class CorsConfigure
{
    // TODO
    public static IApplicationBuilder AddCustomCors(IApplicationBuilder builder)
    {
        return builder.UseCors(b => b
            .WithOrigins("http://localhost:5173", "http://localhost:5172")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
    }
}