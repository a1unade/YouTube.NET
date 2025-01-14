using MediatR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses.Playlist;
using YouTube.Application.DTOs.Playlist;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.Playlist.GetPlaylistById;

public class GetPlaylistByIdQueryHandler : IRequestHandler<GetPlaylistByIdQuery, PlaylistResponse>
{
    private readonly IDbContext _context;

    public GetPlaylistByIdQueryHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<PlaylistResponse> Handle(GetPlaylistByIdQuery request, CancellationToken cancellationToken)
    {
        var playlist = await _context.Playlists
                           .Include(x => x.Videos)
                           .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                       ?? throw new NotFoundException(typeof(Domain.Entities.Playlist));

        if (playlist.IsHidden)
            return new PlaylistResponse { IsSuccessfully = true, Message = PlaylistMessages.PlaylistIsPrivate };

        if (playlist.Videos == null || !playlist.Videos.Any())
            return new PlaylistResponse { IsSuccessfully = true, Message = PlaylistMessages.PlaylistIsEmpty };

        var videoDto = playlist.Videos.Select(video => new VideoDto
        {
            VideoFileId = video.VideoUrlId,
            PreviewId = video.PreviewImgId,
            ViewCount = video.ViewCount,
            Name = video.Name,
            RealiseDate = video.ReleaseDate
        }).ToList();

        return new PlaylistResponse
        {
            IsSuccessfully = true,
            PlaylistDto = new PlaylistDto
            {
                Name = playlist.Name,
                Description = playlist.Description,
                VideoDto = videoDto
            }
        };
    }
}