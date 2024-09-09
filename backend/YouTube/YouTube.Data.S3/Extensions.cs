using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using YouTube.Application.Interfaces;

namespace YouTube.Data.S3;

public static class Extensions
{
    public static void AddS3Storage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStorage(configuration);
    }

    private static void AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMinio(configureClient => configureClient
            .WithEndpoint(configuration["S3Storage:Endpoint"])
            .WithCredentials(configuration["S3Storage:AccessKey"], configuration["S3Storage:SecretKey"])
            .WithSSL(false)
            .Build());

        services.AddScoped<IS3Service, S3Service>();
    }
}