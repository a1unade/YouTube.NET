namespace YouTube.Application.Common.Responses.VideoResponse;

public class VideoListResponse : BaseResponse
{
    public List<VideoItem>? VideoItems { get; set; }
}