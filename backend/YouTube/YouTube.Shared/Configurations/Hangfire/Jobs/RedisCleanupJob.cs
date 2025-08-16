using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using YouTube.Application.Interfaces;

namespace YouTube.Shared.Configurations.Hangfire.Jobs;

public class RedisCleanupJob(
    IDbContext context,
    IDistributedCache distributedCache,
    IConfiguration configuration,
    ILogger<RedisCleanupJob> logger) 
    : IWorker
{
    private readonly string _redisConnectionString = configuration.GetConnectionString("Redis")!;

    [AutomaticRetry(Attempts = 3)]
    [JobDisplayName("Redis Cleanup Job")]
    public async Task ExecuteAsync()
    {
        var redisKeys = await GetAllRedisKeysAsync();
        logger.LogInformation("Found {Count} keys in Redis", redisKeys.Count);

        var redisFileIds = redisKeys
            .Select(x => x.Replace("file:", ""))
            .Where(x => Guid.TryParse(x, out _))
            .Select(Guid.Parse)
            .ToList();

        if (!redisFileIds.Any()) 
            return;

        var existingFileIds = await context.Files
            .Where(x => redisFileIds.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync();

        var fileIdsToRemove = redisFileIds.Except(existingFileIds).ToList();
        
        foreach (var fileId in fileIdsToRemove)
        {
            var key = $"file:{fileId}";
            await distributedCache.RemoveAsync(key);
            logger.LogInformation("Removed Redis key {Key} for missing file {FileId}", key, fileId);
        }
    }

    private async Task<List<string>> GetAllRedisKeysAsync()
    {
        await using var redis = await ConnectionMultiplexer.ConnectAsync(_redisConnectionString);
        var server = redis.GetServer(redis.GetEndPoints().First());
        return server.Keys(pattern: "file:*").Select(k => k.ToString()).ToList();
    }
}