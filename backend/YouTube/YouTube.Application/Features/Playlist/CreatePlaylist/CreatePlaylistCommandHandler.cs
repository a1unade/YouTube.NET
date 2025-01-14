using MediatR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.Playlist.CreatePlaylist;

public class CreatePlaylistCommandHandler : IRequestHandler<CreatePlaylistCommand, BaseResponse>
{ 
    private readonly IVideoRepository _videoRepository;
    private readonly IDbContext _context;

    public CreatePlaylistCommandHandler(IVideoRepository videoRepository,
        IDbContext context)
    {
        _videoRepository = videoRepository;
        _context = context;
    }

    public async Task<BaseResponse> Handle(CreatePlaylistCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || request.ChannelId == Guid.Empty)
            throw new ValidationException();

        var channel = await _context.Channels.FirstOrDefaultAsync(x => x.Id == request.ChannelId, cancellationToken) ??
                      throw new NotFoundException(typeof(Domain.Entities.Channel));

        if (channel is null)
            throw new NotFoundException(typeof(Domain.Entities.Channel));

        var playlist = new Domain.Entities.Playlist
        {
            IsHidden = request.IsHidden,
            Name = request.Name,
            Description = !string.IsNullOrWhiteSpace(request.Description) ? request.Description : null,
            CreateDate = DateOnly.FromDateTime(DateTime.Now),
            ChannelId = channel.Id,
            Channel = channel
        };

        if (request.VideosId != null && request.VideosId.Any())
        {
            var videos = await _videoRepository.GetVideosByIds(request.VideosId!, cancellationToken);
            playlist.Videos = videos;
        }

        await _context.Playlists.AddAsync(playlist, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new BaseResponse
        {
            IsSuccessfully = true,
            EntityId = playlist.Id
        };
    }
}