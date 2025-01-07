using YouTube.Application.DTOs.Playlist;

namespace YouTube.Application.Common.Responses.Playlist;

public class PlaylistResponse : BaseResponse
{
    public PlaylistDto PlaylistDto { get; set; } = default!;
}