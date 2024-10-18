using MediatR;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses.Video;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using Channel = YouTube.Domain;

namespace YouTube.Application.Features.Video.GetVideoPagination;

public class GetVideoPaginationQueryHandler : IRequestHandler<GetVideoPaginationQuery, VideoPaginationResponse>
{
    private readonly IS3Service _s3Service;
    private readonly IVideoRepository _videoRepository;
    private readonly IGenericRepository<Domain.Entities.Channel> _channelRepository;

    public GetVideoPaginationQueryHandler(
        IS3Service s3Service,
        IVideoRepository videoRepository, 
        IGenericRepository<Domain.Entities.Channel> channelRepository)
    {
        _s3Service = s3Service;
        _videoRepository = videoRepository;
        _channelRepository = channelRepository;
    }
    public async Task<VideoPaginationResponse> Handle(GetVideoPaginationQuery request, CancellationToken cancellationToken)
    {
        if (request.Page <= 0 || request.Size <= 0)
            throw new ValidationException();

        var videos = await _videoRepository.GetVideoPagination(request.Page, request.Size, cancellationToken);

        var paginationDtos = new List<VideoPaginationDto>();
        foreach (var video in videos)
        {
            var preview = await _s3Service.GetFileUrlAsync(video.PreviewImg.BucketName, video.PreviewImg.FileName,
                cancellationToken);

            var channelImage = string.Empty;
            if (video.Channel.MainImgFile != null)
                channelImage = await _s3Service.GetFileUrlAsync(video.Channel.MainImgFile.BucketName,
                    video.Channel.MainImgFile.FileName, cancellationToken);

            paginationDtos.Add(new VideoPaginationDto
            {
                PreviewUrl = preview,
                ChannelImageUrl = channelImage,
                VideoName = video.Name,
                Views = video.ViewCount,
                ReleaseDate = video.ReleaseDate,
                VideoId = video.Id,
                ChannelId = video.ChannelId,
            });
        }

        return new VideoPaginationResponse
        {
            IsSuccessfully = true,
            Videos = paginationDtos
        };
    }
}