using Microsoft.EntityFrameworkCore;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Repositories;

public class ChannelRepository : IChannelRepository
{
    private readonly IDbContext _context;

    public ChannelRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<Channel?> GetByIdWithImg(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Channels
            .AsNoTracking()
            .Include(x => x.MainImgFile)
            .Include(x => x.BannerImg)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public int GetChannelVideoCount(Guid id)
    {
        return _context.Videos.Count(x => x.Channel.Id == id);
    }

    public async Task<Channel?> GetChannelWithLinks(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Channels.AsNoTracking()
            .Include(x => x.Links)
            .Include(x => x.Videos)
            .Include(x => x.Subscribers)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}