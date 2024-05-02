using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;
using YouTube.Infrastructure.Services;
using YouTube.Persistence.Contexts;

namespace YouTube.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddServices();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddSignalR();
        
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        services
            .AddTransient<IMediator, Mediator>()
            .AddTransient<IAuthService, AuthService>()
            .AddTransient<IEmailService, EmailService>();
        services.AddTransient<ISubscriptionService, SubscriptionService>();
    }
}