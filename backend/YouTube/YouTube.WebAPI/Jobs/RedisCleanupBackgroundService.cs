using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Jobs;

public class RedisCleanupBackgroundService : BackgroundService
{
    private readonly IDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cleanupInterval = TimeSpan.FromDays(1);
    private readonly string _redisConnectionString;
    private readonly ILogger<RedisCleanupBackgroundService> _logger;

    public RedisCleanupBackgroundService(
        IDbContext context,
        IDistributedCache cache,
        string redisConnectionString,
        ILogger<RedisCleanupBackgroundService> logger)
    {
        _context = context;
        _cache = cache;
        _redisConnectionString = redisConnectionString;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var redisKeys = await GetAllRedisKeysAsync();
                _logger.LogInformation($"Найдено {redisKeys.Count} ключей в Redis.");

                var redisFileIds = redisKeys
                    .Select(x => x.Replace("file:", ""))
                    .Where(x => Guid.TryParse(x, out _))
                    .Select(Guid.Parse)
                    .ToList();

                if (redisFileIds.Any())
                {
                    var missingFileIds = await _context.Files
                        .Where(x => redisFileIds.Contains(x.Id))
                        .Select(x => x.Id)
                        .ToListAsync(stoppingToken);
                    
                    var fileIdsToRemove = redisFileIds.Except(missingFileIds).ToList();
                    
                    foreach (var fileId in fileIdsToRemove)
                    {
                        var key = $"file:{fileId}";
                        await _cache.RemoveAsync(key, stoppingToken);
                        _logger.LogInformation($"Ключ {key} удален из Redis, так как файл с ID {fileId} отсутствует в базе данных.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при очистке Redis.");
            }

            await Task.Delay(_cleanupInterval, stoppingToken);
        }
    }

    private async Task<List<string>> GetAllRedisKeysAsync()
    {
        var redis = await ConnectionMultiplexer.ConnectAsync(_redisConnectionString);
        var server = redis.GetServer(redis.GetEndPoints().First());
        var keys = server.Keys(pattern: "file:*").Select(k => k.ToString()).ToList();
        return keys;
    }
}