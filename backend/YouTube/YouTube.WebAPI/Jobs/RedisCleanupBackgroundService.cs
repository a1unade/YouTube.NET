using Microsoft.Extensions.Caching.Distributed;

namespace YouTube.WebAPI.Jobs;

public class RedisCleanupBackgroundService : BackgroundService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<RedisCleanupBackgroundService> _logger;

    public RedisCleanupBackgroundService(IDistributedCache cache, ILogger<RedisCleanupBackgroundService> logger)
    {
        _cache = cache;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _cache.RemoveAsync("upload_counter", stoppingToken);
                _logger.LogInformation("Redis cache cleared at {Time}", DateTime.UtcNow);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error clearing Redis cache");
            }
            
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}