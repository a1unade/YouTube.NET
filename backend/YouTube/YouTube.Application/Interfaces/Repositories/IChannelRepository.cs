using YouTube.Application.Common.Responses.ChannelResponse;
using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

public interface IChannelRepository
{
    Task CreateChannel(string name, Guid userId, CancellationToken cancellationToken);
    Task<Channel?> GetByName(string name, CancellationToken cancellationToken);
    Task<Channel?> GetById(int id, CancellationToken cancellationToken);
    Task<Channel> GetByUser(Guid userId, CancellationToken cancellationToken);
}