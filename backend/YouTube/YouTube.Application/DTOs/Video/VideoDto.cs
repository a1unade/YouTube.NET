namespace YouTube.Application.DTOs.Video;

public class VideoDto
{
    public Guid VideoFileId { get; set; } 

    public Guid PreviewId { get; set; } 
    
    public int ViewCount { get; set; }

    public string Name { get; set; } = default!;
    
    public DateOnly RealiseDate { get; set; }

    public string ChannelName { get; set; } = default!;
}