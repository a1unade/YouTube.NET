using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;
using Serilog.Sinks.SystemConsole.Themes;

namespace YouTube.Shared.Serilog;

public static class SerilogExtensions
{
    public static void AddSerilog(this IServiceCollection services)
    {
        services.AddSingleton<DiagnosticContext>();
        
        Log.Logger = ConfigureSerilog().CreateLogger();

        services.AddLogging(log =>
        {
            log.ClearProviders();
            log.AddSerilog(dispose: true);
        });
    }

    private static LoggerConfiguration ConfigureSerilog()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "WebApi")
            .WriteTo.Console(
                theme: AnsiConsoleTheme.Literate,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .WriteTo.File(
                path: "Logs/webapp.log",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {Message:lj}{NewLine}{Exception}"
            );
    }
}