using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using YouTube.Application.Interfaces;
using YouTube.Data.S3.Options;
using YouTube.Data.S3.Services;

namespace YouTube.Data.S3.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddS3Storage(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("S3Storage").Get<MinioOptions>()!;

        Console.WriteLine(options.AccessKey);
        Console.WriteLine(options.EndPoint);
        Console.WriteLine(options.SecretKey);

        services.AddMinio(configureClient => configureClient
            .WithEndpoint(options.EndPoint)
            .WithCredentials(options.AccessKey, options.SecretKey)
            .WithSSL(false)
            .Build());

        services.AddScoped<IS3Service, S3Service>();
        
    }
}