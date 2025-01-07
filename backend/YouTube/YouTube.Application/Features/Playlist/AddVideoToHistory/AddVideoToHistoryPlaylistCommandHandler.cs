using MediatR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.Playlist.AddVideoToHistory;

public class AddVideoToHistoryPlaylistCommandHandler : IRequestHandler<AddVideoToHistoryPlaylistCommand, BaseResponse>
{
    private readonly IDbContext _context;

    public AddVideoToHistoryPlaylistCommandHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<BaseResponse> Handle(AddVideoToHistoryPlaylistCommand request, CancellationToken cancellationToken)
    {
        var playlist = await _context.Playlists.FirstOrDefaultAsync(x => x.ChannelId == request.ChannelId && x.Name == PlaylistMessages.HistoryPlaylist,
                           cancellationToken)
                       ?? throw new NotFoundException(typeof(Domain.Entities.Playlist));

        var video = await _context.Videos.FirstOrDefaultAsync(x => x.Id == request.VideoId, cancellationToken) 
                    ?? throw new NotFoundException(typeof(Domain.Entities.Video));

        if (playlist.Videos == null)
            playlist.Videos = new List<Domain.Entities.Video>();
        
        playlist.Videos.Add(video);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return new BaseResponse { IsSuccessfully = true };
    }
}