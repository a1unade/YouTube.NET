// using Microsoft.EntityFrameworkCore;
// using YouTube.Application.Common.Responses;
// using YouTube.Application.Interfaces.Repositories;
// using YouTube.Domain.Entities;
// using YouTube.Persistence.Contexts;
//
// namespace YouTube.Persistence.Repositories;
//
// public class CommentRepository : ICommentRepository
// {
//     private readonly ApplicationDbContext _context;
//
//     public CommentRepository(ApplicationDbContext context)
//     {
//         _context = context;
//     }
//     public async Task<List<Comment>> GetVideoComment(Guid videoId, CancellationToken cancellationToken)
//     {
//         var result =  await _context.Comments
//             .Where(x => x.VideoId == videoId)
//             .Include(x => x.Video)
//             .Include(x => x.UserInfo)
//             .ToListAsync(cancellationToken);
//
//         return result;
//     }
//
//     public async Task AddComment(string text, int videoId, Guid userId, CancellationToken cancellationToken)
//     {
//         var comment = new Comment()
//         {
//             CommentText = text,
//             VideoId = videoId,
//             UserId = userId,
//             PostDate = DateTime.Today
//         };
//
//         await _context.Comments.AddAsync(comment, cancellationToken);
//         await _context.SaveChangesAsync(cancellationToken);
//     }
// }