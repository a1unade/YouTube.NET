using MediatR;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.Playlist.CreatePlaylist;

public class CreatePlaylistCommandHandler : IRequestHandler<CreatePlaylistCommand, BaseResponse>
{
    private readonly IChannelRepository _channelRepository;
    private readonly IVideoRepository _videoRepository;
    private readonly IGenericRepository<Domain.Entities.Playlist> _repository;

    public CreatePlaylistCommandHandler(IChannelRepository channelRepository, IVideoRepository videoRepository, IGenericRepository<Domain.Entities.Playlist> repository)
    {
        _channelRepository = channelRepository;
        _videoRepository = videoRepository;
        _repository = repository;
    }
    
    public async Task<BaseResponse> Handle(CreatePlaylistCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || request.ChannelId == Guid.Empty)
            throw new ValidationException();

        var channel = await _channelRepository.GetByIdWithImg(request.ChannelId, cancellationToken);

        if (channel is null)
            throw new NotFoundException(typeof(Domain.Entities.Channel));

        var videos = await _videoRepository.GetVideosByIds(request.VideosId, cancellationToken);

        var playlist = new Domain.Entities.Playlist
        {
            IsHidden = request.IsHidden,
            Name = request.Name,
            Description = !string.IsNullOrWhiteSpace(request.Description)
                ? request.Description
                : null,
            CreateDate = DateOnly.FromDateTime(DateTime.Now),
            Videos = videos,
            ChannelId = channel.Id,
            Channel = channel
        };

        await _repository.Add(playlist, cancellationToken);

        return new BaseResponse
        {
            IsSuccessfully = true,
            EntityId = playlist.Id
        };
    }
}