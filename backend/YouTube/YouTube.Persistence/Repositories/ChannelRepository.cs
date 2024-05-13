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
}