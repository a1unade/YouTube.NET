using YouTube.Application.DTOs.Video;

namespace YouTube.Application.DTOs.Playlist;

public class PlaylistDto
{
    public string Name { get; set; } = default!;
    
    public string? Description { get; set; }

    public List<VideoDto> VideoDto { get; set; } = default!;
}