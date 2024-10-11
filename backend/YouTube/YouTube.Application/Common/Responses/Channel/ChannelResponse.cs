using YouTube.Application.DTOs.Channel;

namespace YouTube.Application.Common.Responses.Channel;

public class ChannelResponse : BaseResponse
{
    public ChannelDto Channel { get; set; } = default!;
}