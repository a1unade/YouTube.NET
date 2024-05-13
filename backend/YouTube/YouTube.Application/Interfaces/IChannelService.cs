using YouTube.Application.Common.Responses;
using YouTube.Application.Common.Responses.ChannelResponse;
using YouTube.Application.DTOs.Channel;

namespace YouTube.Application.Interfaces;

public interface IChannelService
{
    Task<BaseResponse> CreateChannel(CreateChannelDto request, CancellationToken cancellationToken);
    Task<ChannelItemResponse> GetByName(string name, CancellationToken cancellationToken);

    Task<ChannelVideosResponse> GetChannelVideo(int id, CancellationToken cancellationToken);
}