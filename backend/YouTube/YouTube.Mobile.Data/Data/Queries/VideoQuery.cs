using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses.Video;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Mobile.Data.Data.Queries;

/// <summary>
/// 
/// </summary>
[ExtendObjectType("Query")]
public class VideoQuery
{
    [GraphQLDescription("Получить видео по Id")]
    public async Task<VideoResponse> GetVideo(
        [ID] Guid id,
        [Service] IDbContext context,
        CancellationToken cancellationToken
    )
    {
        var video = await context.Videos
            .AsNoTracking()
            .Include(x => x.VideoUrl)
            .Include(x => x.PreviewImg)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (video is null)
        {
            return new VideoResponse
            {
                IsSuccessfully = false,
                Message = "Видео не найдено"
            };
        }
        
        var channel = await context.Channels.FirstOrDefaultAsync(x => x.Id == video.ChannelId, cancellationToken);

        if (channel is null)
        {
            return new VideoResponse
            {
                IsSuccessfully = false,
                Message = ChannelErrorMessage.ChannelNotFound
            };
        }
        

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

    [GraphQLDescription("Получить список видео пагинация")]
    public async Task<VideoPaginationResponse> GetVideoPagination(
        int page,
        int size,
        string? sort,
        string? category,
        [Service] IVideoRepository videoRepository,
        CancellationToken cancellationToken
        )
    {
        var videos = await videoRepository.GetVideoPagination(
            page,
            size,
            category,
            sort,
            cancellationToken);

        var videoListDto = new List<VideoPaginationDto>();
        
        foreach (var video in videos)
        {
            var preview = video.PreviewImgId;


            Guid channelImage = Guid.Empty;

            if (video.Channel.MainImgId != null)
            {
                channelImage = (Guid)video.Channel.MainImgId!;

            }

            videoListDto.Add(new VideoPaginationDto
            {
                PreviewUrl = preview.ToString(),
                ChannelImageUrl = channelImage.ToString(),
                VideoName = video.Name,
                Views = video.ViewCount,
                ReleaseDate = video.ReleaseDate,
                VideoId = video.Id,
                ChannelId = video.ChannelId
            });
        }

        return new VideoPaginationResponse
        {
            IsSuccessfully = true,
            Videos = videoListDto
        };
    }
}