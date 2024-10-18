using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

public interface IVideoRepository
{
    Task<List<Video>> GetVideoPagination(int page, int size, CancellationToken cancellationToken);
}