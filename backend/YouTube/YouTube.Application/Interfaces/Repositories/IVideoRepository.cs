using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

public interface IVideoRepository
{ 
    Task<Video?> GetById(int id, CancellationToken cancellationToken);

    Task<Video?> GetByName(string name, CancellationToken cancellationToken);

    Task<List<Video>?> GetVideoChannel(int id, CancellationToken cancellationToken);

    Task<List<Video>> GetRandomVideo(CancellationToken cancellationToken);
}