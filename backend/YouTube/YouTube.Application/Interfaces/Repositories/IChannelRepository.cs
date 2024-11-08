using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

public interface IChannelRepository
{
    Task<Channel?> GetById(Guid id, CancellationToken cancellationToken);

    public int GetChannelVideoCount(Guid id);

    Task<Channel?> GetChannelWithLinks(Guid id, CancellationToken cancellationToken);
}