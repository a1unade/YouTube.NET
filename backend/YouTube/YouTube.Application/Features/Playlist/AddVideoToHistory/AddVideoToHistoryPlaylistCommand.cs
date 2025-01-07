using MediatR;
using YouTube.Application.Common.Requests.Playlist;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Playlist.AddVideoToHistory;

public class AddVideoToHistoryPlaylistCommand : AddVideoToPlaylistRequest, IRequest<BaseResponse>
{
    public AddVideoToHistoryPlaylistCommand(AddVideoToPlaylistRequest request) : base(request)
    {
        
    }
}