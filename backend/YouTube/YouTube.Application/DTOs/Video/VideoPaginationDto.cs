
namespace YouTube.Application.DTOs.Video;

public class VideoPaginationDto
{
    public string PreviewUrl { get; set; } = default!;
    
    public string ChannelImageUrl { get; set; } = default!;

    public string VideoName { get; set; } = default!;
    
    public int Views { get; set; }
    
    public DateOnly ReleaseDate { get; set; }
    
    public Guid VideoId { get; set; }
    
    public Guid ChannelId { get; set; }
}