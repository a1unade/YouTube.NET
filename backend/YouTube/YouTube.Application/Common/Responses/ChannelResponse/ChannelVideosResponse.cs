using YouTube.Application.Common.Responses.VideoResponse;
using YouTube.Domain.Entities;

namespace YouTube.Application.Common.Responses.ChannelResponse;

public class ChannelVideosResponse : BaseResponse
{
    public List<VideoItem> Videos { get; set; } = default!;
}