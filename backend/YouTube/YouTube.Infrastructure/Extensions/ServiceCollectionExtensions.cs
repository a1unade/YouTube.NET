using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouTube.Application.Interfaces;
using YouTube.Infrastructure.Options;
using YouTube.Infrastructure.Services;

namespace YouTube.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddMessageBus(configuration);
    }

    private static void AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<IMediator, Mediator>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<IJwtGenerator, JwtGenerator>()
            .AddScoped<IPlaylistService, PlaylistService>()
            .AddScoped<IChatService, ChatService>()
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
}