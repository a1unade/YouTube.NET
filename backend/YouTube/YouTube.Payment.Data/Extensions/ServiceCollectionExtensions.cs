using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YouTube.Payment.Data.Context;
using YouTube.Payment.Data.Interfaces;
using YouTube.Payment.Data.Repositories;
using YouTube.Payment.Data.Tools;

namespace YouTube.Payment.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPaymentDbContext(this IServiceCollection services)
    {
        services.AddDbContext<PaymentContext>(options =>
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PaymentDb");
        
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null);
                npgsqlOptions.MigrationsAssembly(typeof(PaymentContext).Assembly.FullName);
            });
        });
    
        services.AddScoped<IPaymentContext, PaymentContext>()
            .AddScoped<IWalletRepository, WalletRepository>()
            .AddScoped<Migrator>();
    }
}