using MediatR;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses.Channel;
using YouTube.Application.DTOs.Channel;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.Channel.GetChannel;

public class GetChannelQueryHandler : IRequestHandler<GetChannelQuery, ChannelResponse>
{
    private readonly IS3Service _s3Service;
    private readonly IChannelRepository _channelRepository;

    public GetChannelQueryHandler(IS3Service s3Service, IChannelRepository channelRepository)
    {
        _s3Service = s3Service;
        _channelRepository = channelRepository;
    }
    public async Task<ChannelResponse> Handle(GetChannelQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            throw new ValidationException();

        var channel = await _channelRepository.GetById(request.Id, cancellationToken);
        
        if (channel is null)
            throw new NotFoundException(ChannelErrorMessage.ChannelNotFound);

        var banner = string.Empty;
        var main = string.Empty;
        
        if (channel.BannerImg is not null)
            banner = await _s3Service.GetFileUrlAsync(channel.BannerImg.BucketName, channel.BannerImg.FileName,
                cancellationToken);

        if (channel.MainImgFile is not null)
            main = await _s3Service.GetFileUrlAsync(channel.MainImgFile!.BucketName, channel.MainImgId.ToString()!,
                cancellationToken);
        
        return new ChannelResponse
        {
            IsSuccessfully = true,
            Channel = new ChannelDto
            {
                Id = channel.Id,
                Name = channel.Name,
                MainImage = main,
                BannerImage = banner,
                Subscribers = channel.SubCount,
                Description = channel.Description,
                VideoCount = _channelRepository.GetChannelVideoCount(request.Id)
            }
        };
    }
}