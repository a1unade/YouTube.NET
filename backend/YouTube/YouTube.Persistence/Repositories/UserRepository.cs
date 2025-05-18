using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbContext _context;
    private readonly IDistributedCache _cache;

    public UserRepository(IDbContext context,
        IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<User?> FindById(Guid id, CancellationToken cancellationToken)
    {
        var userString = await _cache.GetAsync(id.ToString(), cancellationToken);

        if (userString is not null)
            return JsonSerializer.Deserialize<User>(userString);
        
        var user = await _context.Users
            .AsNoTracking()
            .Include(x => x.UserInfo)
            .Include(x => x.Subscriptions)
            .Include(x => x.Channels)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user is not null)
        {
            var userBytes = JsonSerializer.SerializeToUtf8Bytes(user);
            await _cache.SetAsync(user.Id.ToString(), userBytes, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            }, cancellationToken);
        }
        
        return user;
    }

    public async Task<User?> FindByEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<User?> GetUserWithChannel(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(x => x.UserInfo)
            .Include(x => x.Subscriptions)
            .Include(x => x.Channels)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}