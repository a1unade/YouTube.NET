using MediatR;
using YouTube.Application.Common.Requests.Playlist;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Playlist.AddVideoToWatchLater;

public class AddVideoToWatchLaterPlaylistCommand : AddVideoToPlaylistRequest, IRequest<BaseResponse>
{
    public AddVideoToWatchLaterPlaylistCommand(AddVideoToPlaylistRequest request) : base(request)
    {
        
    }
}