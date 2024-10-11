namespace YouTube.Application.DTOs.Video;

public class VideoDto
{
    public string VideoUrl { get; set; } = default!;

    public string PreviewUrl { get; set; } = default!;
    
    public int ViewCount { get; set; }

    public string Name { get; set; } = default!;
    
    public DateOnly RealiseDate { get; set; }

    public string ChannelName { get; set; } = default!;
}