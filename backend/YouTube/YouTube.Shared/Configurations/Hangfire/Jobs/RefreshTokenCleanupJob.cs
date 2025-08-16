using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YouTube.Application.Interfaces;

namespace YouTube.Shared.Configurations.Hangfire.Jobs;

public class RefreshTokenCleanupJob(IDbContext context, ILogger<RefreshTokenCleanupJob> logger) : IWorker
{
    private const int KeepExpiredDays = 7; // Храним просроченные 7 дней
    private const int KeepRevokedDays = 3; // Храним отозванные 3 дня
    
    [AutomaticRetry(Attempts = 0)]
    public async Task ExecuteAsync()
    {
        logger.LogInformation("RefreshTokenCleanupJob started");
        
        try
        {
            var utcNow = DateTime.UtcNow;
            var expiredCutoff = utcNow.AddDays(-KeepExpiredDays);
            var revokedCutoff = utcNow.AddDays(-KeepRevokedDays);

            // Удаляем просроченные токены старше 7 дней
            var expiredCount = await context.RefreshTokens
                .Where(t => t.Expires < expiredCutoff)
                .ExecuteDeleteAsync();

            // Удаляем отозванные токены старше 3 дней или которые были заменены
            var revokedCount = await context.RefreshTokens
                .Where(t => t.Revoked != null && 
                            t.Revoked < revokedCutoff ||
                            t.ReplacedByToken != null)
                .ExecuteDeleteAsync();

            logger.LogInformation("Removed {Expired} expired and {Revoked} revoked tokens", 
                expiredCount, revokedCount);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to cleanup refresh tokens");
            throw; 
        }
    }
}