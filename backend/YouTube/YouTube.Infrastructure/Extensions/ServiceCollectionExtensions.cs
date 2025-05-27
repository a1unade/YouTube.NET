using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouTube.Application.Interfaces;
using YouTube.Infrastructure.Helpers;
using YouTube.Infrastructure.Options;
using YouTube.Infrastructure.Services;

namespace YouTube.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddMessageBus(configuration);
        services.AddGrpcClient(configuration);
    }

    private static void AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<IMediator, Mediator>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<IJwtGenerator, JwtGenerator>()
            .AddScoped<IPlaylistService, PlaylistService>()
            .AddScoped<IChatService, ChatService>()
            .AddScoped<IFileService, FileService>()
            .AddSingleton<IChatConnectionManager, ChatConnectionManager>()
            .AddSignalR();
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
    
    private static void AddGrpcClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<Proto.ChatService.ChatServiceClient>(options =>
        {
            options.Address = new Uri(configuration["ChatService:GrpcEndpoint"]!);
        });
    }   
}