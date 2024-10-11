using YouTube.Application.DTOs.Video;

namespace YouTube.Application.Common.Responses.Video;

public class VideoResponse : BaseResponse
{
    public VideoDto Video { get; set; } = default!;

    public Guid ChannelId { get; set; }
    
    public Guid VideoId { get; set; }
}