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
        var channel = new Channel()
        {
            Name = name,
            CreateDate = DateTime.Today,
            UserId = userId
        };
        await _context.Channels.AddAsync(channel, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Channel?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _context.Channels.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Channel?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Channels
            .Where(x => x.Name == name)
            .Include(x => x.UserInfo)
            .Include(x => x.Videos)
            .Include(x => x.MainImgFile)
            .Include(x => x.BannerImgFile)
            .FirstOrDefaultAsync(cancellationToken);
        
    }

    public async Task<Channel> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        var res = await _context.Channels
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken) 
                  ?? new Channel();

        return res;
    }
}