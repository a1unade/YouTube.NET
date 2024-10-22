using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Repositories;
[ExcludeFromCodeCoverage]
public class VideoRepository : IVideoRepository
{
    private readonly IDbContext _context;

    public VideoRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Video>> GetVideoPagination(int page, int size, string? category, string? sort, CancellationToken cancellationToken)
    {
        
        var query = _context.Videos
            .AsNoTracking()
            .Include(x => x.PreviewImg)
            .Include(x => x.Channel)
            .ThenInclude(x => x.MainImgFile)
            .AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            var categoryVideo = await _context.Categories.FirstOrDefaultAsync(x => x.Name == category, cancellationToken);
            query = query.Where(x => Equals(x.Category!.Name, categoryVideo));
        }
        
        if (!string.IsNullOrEmpty(sort))
        {
            query = sort switch
            {
                "view" => query.OrderByDescending(x => x.ViewCount),
                "like" => query.OrderByDescending(x => x.LikeCount),
                "dislike" => query.OrderByDescending(x => x.DisLikeCount),
                _ => query
            };
        }
        
        return await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);
    }
}