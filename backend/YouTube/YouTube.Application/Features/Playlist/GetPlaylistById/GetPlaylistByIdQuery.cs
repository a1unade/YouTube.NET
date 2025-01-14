using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Responses.Playlist;

namespace YouTube.Application.Features.Playlist.GetPlaylistById;

public class GetPlaylistByIdQuery : IdRequest, IRequest<PlaylistResponse>
{
    public GetPlaylistByIdQuery(IdRequest request) : base(request)
    {
        
    }
}