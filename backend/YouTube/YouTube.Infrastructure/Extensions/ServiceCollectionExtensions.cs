using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using YouTube.Application.Interfaces;
using YouTube.Infrastructure.Options;
using YouTube.Infrastructure.Services;

namespace YouTube.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddS3Storage(configuration);
        services.AddMessageBus(configuration);
    }

    private static void AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<IMediator, Mediator>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<IJwtGenerator, JwtGenerator>()
            .AddScoped<IChatService, ChatService>()
            .AddSignalR();
    }

    private static void AddS3Storage(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("S3Storage").Get<MinioOptions>()!;

        services.AddMinio(configureClient => configureClient
            .WithEndpoint(options.EndPoint)
            .WithCredentials(options.AccessKey, options.SecretKey)
            .WithSSL(false)
            .Build());

        services.AddScoped<IS3Service, S3Service>();
    }
    
    private static void AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(nameof(RabbitOptions)).Get<RabbitOptions>()!;
        
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host($"rabbitmq://{options.Hostname}", h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });
                
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}