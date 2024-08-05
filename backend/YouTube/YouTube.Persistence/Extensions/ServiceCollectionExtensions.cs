using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Persistence.Contexts;
using YouTube.Persistence.Repositories;

namespace YouTube.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddHttpContextAccessor();
    }
    
    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");
    
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString,
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IDbContext, ApplicationDbContext>()
            .AddScoped<IChannelRepository, ChannelRepository>();

    }
}