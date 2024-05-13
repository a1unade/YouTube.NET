using YouTube.Application.Common.Responses.ChannelResponse;

namespace YouTube.Application.Interfaces;

public interface IChannelService
{
    Task<ChannelItemResponse> GetByName(string name, CancellationToken cancellationToken);

    Task<ChannelVideosResponse> GetChannelVideo(int id, CancellationToken cancellationToken);
}