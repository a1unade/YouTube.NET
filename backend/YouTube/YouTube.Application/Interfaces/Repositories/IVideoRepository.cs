using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

public interface IVideoRepository
{ 
    Task<Video?> GetById(Guid id, CancellationToken cancellationToken);

    Task<Video?> GetByName(string name, CancellationToken cancellationToken);

    Task<List<Video>?> GetVideoChannel(Guid id, CancellationToken cancellationToken);

    Task<List<Video>> GetRandomVideo(CancellationToken cancellationToken);
}