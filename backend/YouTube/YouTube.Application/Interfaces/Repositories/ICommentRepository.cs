using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<List<Comment>> GetVideoComment(int videoId, CancellationToken cancellationToken);
}