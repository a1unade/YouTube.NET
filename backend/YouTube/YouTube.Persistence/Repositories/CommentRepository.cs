using Microsoft.EntityFrameworkCore;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;
using YouTube.Persistence.Contexts;

namespace YouTube.Persistence.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Comment>> GetVideoComment(int videoId, CancellationToken cancellationToken)
    {
        var result =  await _context.Comments
            .Where(x => x.VideoId == videoId)
            .Include(x => x.Video)
            .Include(x => x.UserInfo)
            .ToListAsync(cancellationToken);

        return result;
    }
}