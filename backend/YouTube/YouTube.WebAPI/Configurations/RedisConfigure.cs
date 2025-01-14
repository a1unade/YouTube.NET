using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace YouTube.WebAPI.Configurations;

public static class RedisConfigure
{
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "Redis";
        }); 
    }
}