using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Jobs;

public class RedisCleanupBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _cleanupInterval = TimeSpan.FromDays(1);
    private readonly string _redisConnectionString = "localhost:6379";
    private readonly ILogger<RedisCleanupBackgroundService> _logger;

    public RedisCleanupBackgroundService(
        IServiceProvider serviceProvider,
        ILogger<RedisCleanupBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<IDbContext>();
                var cache = scope.ServiceProvider.GetRequiredService<IDistributedCache>();

                var redisKeys = await GetAllRedisKeysAsync();
                _logger.LogInformation($"Найдено {redisKeys.Count} ключей в Redis.");

                var redisFileIds = redisKeys
                    .Select(x => x.Replace("file:", ""))
                    .Where(x => Guid.TryParse(x, out _))
                    .Select(Guid.Parse)
                    .ToList();

                if (redisFileIds.Any())
                {
                    var missingFileIds = await context.Files
                        .Where(x => redisFileIds.Contains(x.Id))
                        .Select(x => x.Id)
                        .ToListAsync(stoppingToken);
                    
                    var fileIdsToRemove = redisFileIds.Except(missingFileIds).ToList();
                    
                    foreach (var fileId in fileIdsToRemove)
                    {
                        var key = $"file:{fileId}";
                        await cache.RemoveAsync(key, stoppingToken);
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