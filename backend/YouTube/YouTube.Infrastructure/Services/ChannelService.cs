using YouTube.Application.Common.Responses;
using YouTube.Application.Common.Responses.ChannelResponse;
using YouTube.Application.Common.Responses.VideoResponse;
using YouTube.Application.DTOs.Channel;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Infrastructure.Services;

public class ChannelService : IChannelService
{
    private readonly IChannelRepository _channelRepository;
    private readonly IVideoRepository _videoRepository;

    public ChannelService(IChannelRepository channelRepository, IVideoRepository videoRepository)
    {
        _channelRepository = channelRepository;
        _videoRepository = videoRepository;
    }

    public async Task<BaseResponse> CreateChannel(CreateChannelDto request, CancellationToken cancellationToken)
    {
        await _channelRepository.CreateChannel(request.ChannelName, request.UserId, cancellationToken);

        return new BaseResponse()
        {
            IsSuccessfully = true
        };
    }
    public async Task<ChannelItemResponse> GetByName(string name, CancellationToken cancellationToken)
    {
        var channel = await _channelRepository.GetByName(name, cancellationToken);
        
        if (channel == null!)
        {
            return new ChannelItemResponse
            {
                Error = new List<string> { "Channel not found" }
            };
        }


        if(channel.Videos.Count == 0)
            return new ChannelItemResponse
            {
                IsSuccessfully = true,
                Channel = new ChannelItem
                {
                    Id = channel.Id,
                    Name = channel.Name,
                    Subscription = channel.SubCount,
                    Description = channel.Description,
                    Createdate = channel.CreateDate,
                    MainImg = channel.MainImgFile.Path ?? null,
                    BannerImg = channel.BannerImgFile.Path ?? null,
                    VideoCount = channel.Videos.Count,
                    ViewCount = 0,
                    Country = "Russia"
                }
            };

        int viewCount = 0;
        foreach (var video in channel.Videos)
        {
            viewCount += video.ViewCount;
        }
        
        return new ChannelItemResponse
        {
            IsSuccessfully = true,
            Channel = new ChannelItem
            {
                Id = channel.Id,
                Name = channel.Name,
                Subscription = channel.SubCount,
                Description = channel.Description,
                Createdate = channel.CreateDate,
                MainImg = channel.MainImgFile.Path ?? null,
                BannerImg = channel.BannerImgFile.Path ?? null,
                VideoCount = channel.Videos.Count,
                ViewCount = viewCount, 
                Country = "Russia"
            }
        };
    }

    public async Task<ChannelVideosResponse> GetChannelVideo(int id, CancellationToken cancellationToken)
    {
        var result = await _videoRepository.GetVideoChannel(id, cancellationToken);
        
        if (result?.Count == 0)
        {
            return new ChannelVideosResponse()
            {
                IsSuccessfully = true,
                Message = "0 Videos",
                Videos = new List<VideoItem>()
            };
        }

        var videos = new List<VideoItem>();

        foreach (var video in result!)
        {
            videos.Add(new VideoItem()
            {
                Id = video.Id,
                Description = video.Description,
                DisLikeCount = video.DisLikeCount,
                LikeCount = video.LikeCount,
                Name = video.Name,
                PreviewImg = video.StaticFile.Path ?? null,
                ReleaseDate = video.ReleaseDate,
                ViewCount = video.ViewCount
            });
        }

        return new ChannelVideosResponse()
        {
            IsSuccessfully = true,
            Videos = videos
        };
    }
}