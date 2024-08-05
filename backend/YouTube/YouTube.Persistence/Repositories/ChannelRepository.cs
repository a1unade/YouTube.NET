using Microsoft.EntityFrameworkCore;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;
using YouTube.Persistence.Contexts;

namespace YouTube.Persistence.Repositories;

public class ChannelRepository : IChannelRepository
{
    private readonly ApplicationDbContext _context;

    public ChannelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateChannel(string name, Guid userId, CancellationToken cancellationToken)
    {
       
    }

    public async Task<Channel?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return null;
    }

    public async Task<Channel?> GetByName(string name, CancellationToken cancellationToken)
    {
        return null;

    }

    public async Task<Channel> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        var res = await _context.Channels
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        return res;
    }
}