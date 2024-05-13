using YouTube.Application.Common.Responses.ChannelResponse;
using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

public interface IChannelRepository
{
    Task<Channel?> GetByName(string name, CancellationToken cancellationToken);
}