using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using YouTube.MessageBus.Consumers;
using YouTube.MessageBus.Options;

namespace YouTube.MessageBus.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMessageBus(this IServiceCollection services, RabbitOptions options)
    {
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
}