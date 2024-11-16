using MediatR;
using YouTube.Application.Common.Requests.Playlist;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Playlist.CreatePlaylist;

public class CreatePlaylistCommand : CreatePlaylistRequest, IRequest<BaseResponse>
{
    public CreatePlaylistCommand(CreatePlaylistRequest request) : base(request)
    {
    }
}