using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Responses.Channel;
using YouTube.Application.DTOs.Channel;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Mobile.Data.Data.Queries;

/// <summary>
/// 
/// </summary>
[ExtendObjectType("Query")]
public class ChannelQuery
{
    [GraphQLDescription("Получить канал по Id")]
    public async Task<ChannelResponse> GetChannel(
        [ID] Guid id,
        [Service] IChannelRepository channelRepository,
        CancellationToken cancellationToken
        )
    {
        var channel = await channelRepository.GetByIdWithImg(id, cancellationToken)
                      ?? throw new GraphQLException("Channel not found");
        
        var banner = Guid.Empty;
        var main = Guid.Empty;
        
        if (channel.BannerImgId is not null && channel.BannerImgId != Guid.Empty)
            banner = (Guid)channel.BannerImgId;

        if (channel.MainImgId is not null && channel.MainImgId != Guid.Empty)
            main = (Guid)channel.MainImgId;
        return new ChannelResponse
        {
            IsSuccessfully = true,
            Channel =  new ChannelDto
            {
                Id = channel.Id,
                Name = channel.Name,
                MainImage = main.ToString(),
                BannerImage = banner.ToString(),
                Subscribers = channel.SubCount,
                Description = channel.Description,
                VideoCount = channelRepository.GetChannelVideoCount(id)
            }
        };
    }

    [GraphQLDescription("Получить каналы на которые подписан пользователь с пагинацией")]
    public async Task<ChannelSubscriptionsResponse> GetSubsChannel(
        [ID] Guid id,
        int page,
        int size,
        [Service] IDbContext context,
        CancellationToken cancellationToken
    )
    {
       var channelSubs = await context.ChannelSubscriptions
            .Where(x => x.SubscriberChannelId == id)
            .OrderBy(x => x.SubscribedTo.Name) 
            .Include(x => x.SubscribedTo)
            .Skip((page - 1) * size)
            .Take(size)
            .Select(x => new 
            {
                Channel = x.SubscribedTo,
                VideoCount = x.SubscribedTo.Videos.Count 
            })
            .ToListAsync(cancellationToken);

        var channelDtos = new List<ChannelDto>();
        foreach (var item in channelSubs)
        {
            var bannerUrl = item.Channel.BannerImgId;

            var mainImageUrl = item.Channel.MainImgId;

            channelDtos.Add(new ChannelDto
            {
                Id = item.Channel.Id,
                Name = item.Channel.Name,
                Description = item.Channel.Description ?? string.Empty,
                Subscribers = item.Channel.SubCount,
                VideoCount = item.VideoCount,
                BannerImage = bannerUrl.ToString(),
                MainImage = mainImageUrl.ToString()
            });
        }

        return new ChannelSubscriptionsResponse
        {
            IsSuccessfully = true,
            Channels = channelDtos
        };
    }
}