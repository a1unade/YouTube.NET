using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<List<Comment>> GetVideoComment(int videoId, CancellationToken cancellationToken);

    Task AddComment(string text, int videoId, Guid userId, CancellationToken cancellationToken);
}