using YouTube.Application.DTOs.Channel;

namespace YouTube.Application.Common.Responses.Channel;

public class ChannelSubscriptionsResponse : BaseResponse
{
    public List<ChannelDto> Channels { get; set; } = null!;
}