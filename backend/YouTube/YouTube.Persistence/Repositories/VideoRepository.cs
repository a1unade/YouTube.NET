using Microsoft.EntityFrameworkCore;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Repositories;

public class VideoRepository : IVideoRepository
{
    private readonly IDbContext _context;

    public VideoRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Video>> GetVideoPagination(int page, int size, CancellationToken cancellationToken)
    {
        return await _context.Videos
            .Include(x => x.PreviewImg)
            .Include(x => x.Channel)
            .ThenInclude(x => x.MainImgFile )
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);
    }
}