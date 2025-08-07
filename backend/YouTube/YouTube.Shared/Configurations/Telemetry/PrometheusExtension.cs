using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using Prometheus.SystemMetrics;
using YouTube.Shared.Settings;

namespace YouTube.Shared.Configurations.Telemetry;

public static class PrometheusExtension
{
    public static void AddPrometheus(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("Prometheus").Get<PrometheusSettings>();
        services.AddMetricServer(options =>
        {
            options.Port = (ushort)settings!.Port;
        });

        services.AddSystemMetrics();
    }
}