using YouTube.Application.DTOs.Video;

namespace YouTube.Application.Common.Responses.Video;

public class VideoPaginationResponse : BaseResponse
{
    public List<VideoPaginationDto> Videos { get; set; } = default!;
}