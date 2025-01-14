
namespace YouTube.Application.Common.Requests.Playlist;

public class AddVideoToPlaylistRequest 
{
    public AddVideoToPlaylistRequest()
    {
        
    }

    public AddVideoToPlaylistRequest(AddVideoToPlaylistRequest request)
    {
        ChannelId = request.ChannelId;
        VideoId = request.VideoId;
    }
    
    public Guid ChannelId { get; set; }
    
    public Guid VideoId { get; set; }
}