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
        User? user;
        
        var userString = await _cache.GetStringAsync(id.ToString(), cancellationToken);
        
        if (userString is not null)
        {
            user =  JsonSerializer.Deserialize<User>(userString);
            Console.WriteLine("Чела достали из кеша");
        }
        else
        {
            user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (user is not null)
            {
                userString = JsonSerializer.Serialize(user);
                await _cache.SetStringAsync(user.Id.ToString(), userString, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                }, cancellationToken);
            }

            Console.WriteLine("Из бд достали дауна");
        }
        return user ?? null;
    }

    public async Task<User?> FindByEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken) ?? null;
    }
}