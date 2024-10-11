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
    private readonly IS3Service _s3Service;

    public GetVideoQueryHandler(IDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }
    public async Task<VideoResponse> Handle(GetVideoQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
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
        
        var videoUrl =
            await _s3Service.GetFileUrlAsync(
                video.VideoUrl.BucketName!,
                video.VideoUrl.FileName,
                cancellationToken);

        var previewUrl = await _s3Service.GetFileUrlAsync(
            video.PreviewImg.BucketName!,
            video.PreviewImg.FileName,
            cancellationToken);


        return new VideoResponse
        {
            IsSuccessfully = true,
            ChannelId = video.ChannelId,
            VideoId = video.Id,
            Video = new VideoDto
            {
                VideoUrl = videoUrl,
                PreviewUrl = previewUrl,
                ViewCount = video.ViewCount,
                Name = video.Name,
                RealiseDate = video.ReleaseDate,
                ChannelName = channel.Name
            }
        };
    }
}