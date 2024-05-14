using Microsoft.EntityFrameworkCore;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;
using YouTube.Persistence.Contexts;

namespace YouTube.Persistence.Repositories;

public class VideoRepository : IVideoRepository
{
    private readonly ApplicationDbContext _context;

    public VideoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Video?> GetById(int id, CancellationToken cancellationToken)
    {
        var video = await _context.Videos
                        .Include(x => x.StaticFile)
                        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                    ?? null;

        return video;
    }

    public async Task<Video?> GetByName(string name, CancellationToken cancellationToken)
    {
        var video = await _context.Videos
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken)
                    ?? null;

        return video;
    }

    public async Task<List<Video>?> GetVideoChannel(int id, CancellationToken cancellationToken)
    {
        var result = await _context.Videos
            .Include(x => x.Channel)
            .Include(x => x.StaticFile)
            .Where(x => x.Channel.Id == id)
            .ToListAsync(cancellationToken);

        if (result.Count == 0)
            return new List<Video>();

        return result;
    }

    public async Task<List<Video>> GetRandomVideo(CancellationToken cancellationToken)
    {
        return await _context.Videos.Take(20).ToListAsync(cancellationToken);
    }
}