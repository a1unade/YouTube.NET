namespace YouTube.Application.Common.Responses.ChannelResponse;

public class ChannelResponse : BaseResponse
{
    public List<ChannelItemResponse> Channels { get; set; } = default!;
}