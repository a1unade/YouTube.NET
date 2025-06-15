using MassTransit;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Interfaces;
using YouTube.BusAPI.Consumers;
using YouTube.BusAPI.Interfaces;
using YouTube.BusAPI.Services;
using YouTube.Infrastructure.Options;
using YouTube.Persistence.Contexts;

namespace YouTube.BusAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRabbitMq(configuration);
        services.AddDb(configuration);
        services.AddServices();
        services.AddSignalR();
    }

    private static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(nameof(RabbitOptions)).Get<RabbitOptions>()!;
        
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumer<ChatConsumer>();
            //x.AddConsumer<IncrementViewConsumer>();
            
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

    private static void AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString,
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMessageService, MessageService>()
            .AddScoped<IDbContext, ApplicationDbContext>();
    }
}