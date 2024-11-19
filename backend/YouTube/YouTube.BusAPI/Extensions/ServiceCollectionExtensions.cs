using MassTransit;
using YouTube.BusAPI.Consumers;
using YouTube.BusAPI.Interfaces;
using YouTube.BusAPI.Services;
using YouTube.Infrastructure.Options;

namespace YouTube.BusAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRabbitMq(configuration);
        services.AddServices();
    }

    private static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(nameof(RabbitOptions)).Get<RabbitOptions>()!;
        
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumer<ChatConsumer>();
            
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

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMessageService, MessageService>();
    }
}