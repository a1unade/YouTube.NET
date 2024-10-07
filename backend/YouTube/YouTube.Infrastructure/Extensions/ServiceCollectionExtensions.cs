using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using YouTube.Application.Interfaces;
using YouTube.Infrastructure.Services;

namespace YouTube.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddStorage(configuration);
        services.AddSignalR();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<IMediator, Mediator>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<IJwtGenerator, JwtGenerator>();
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