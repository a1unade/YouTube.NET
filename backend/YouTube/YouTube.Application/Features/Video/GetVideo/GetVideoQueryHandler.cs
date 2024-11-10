using MediatR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses.Video;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.Video.GetVideo;

public class GetVideoQueryHandler : IRequestHandler<GetVideoQuery, VideoResponse>
{
    private readonly IDbContext _context;

    public GetVideoQueryHandler(IDbContext context)
    {
        _context = context;
    }
    public async Task<VideoResponse> Handle(GetVideoQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty || request == null)
            throw new ValidationException();

        var video = await _context.Videos
            .AsNoTracking()
            .Include(x => x.VideoUrl)
            .Include(x => x.PreviewImg)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (video is null)
            throw new NotFoundException("Видео не найдено");

        var channel = await _context.Channels.FirstOrDefaultAsync(x => x.Id == video.ChannelId, cancellationToken);

        if (channel is null)
            throw new NotFoundException(ChannelErrorMessage.ChannelNotFound);

        return new VideoResponse
        {
            IsSuccessfully = true,
            ChannelId = video.ChannelId,
            VideoId = video.Id,
            Video = new VideoDto
            {
                VideoFileId = video.VideoUrl.Id,
                PreviewId = video.PreviewImg.Id,
                ViewCount = video.ViewCount,
                Name = video.Name,
                RealiseDate = video.ReleaseDate,
                ChannelName = channel.Name
            }
        };
    }
}