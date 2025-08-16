using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouTube.Shared.Configurations.Hangfire.Jobs;

namespace YouTube.Shared.Configurations.Hangfire;

public static class HangfireExtension
{
    public static void AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(
                configuration.GetConnectionString("Postgres"),
                new PostgreSqlStorageOptions
                {
                    SchemaName = "hangfire",
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromSeconds(30),
                    InvisibilityTimeout = TimeSpan.FromHours(1)
                }));
        
        services.AddHangfireServer(options =>
        {
            options.ServerName = configuration["Hangfire:ServerName"];
            options.WorkerCount = int.Parse(configuration["Hangfire:WorkerCount"]!);
            options.Queues = [configuration["Hangfire:Queues"]];
        });
    }

    public static void RegisterHangfireJobs(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseHangfireDashboard(configuration["Hangfire:DashboardPath"], new DashboardOptions
        {
            DashboardTitle = configuration["Hangfire:DashboardTitle"]
        });     
        
        RecurringJob.AddOrUpdate<RedisCleanupJob>(
            nameof(RedisCleanupJob),
            job => job.ExecuteAsync(),
            Cron.Daily,
            TimeZoneInfo.Local);
        
        RecurringJob.AddOrUpdate<RefreshTokenCleanupJob>(
            nameof(RefreshTokenCleanupJob),
            job => job.ExecuteAsync(),
            Cron.Daily,
            TimeZoneInfo.Local);
    }
}