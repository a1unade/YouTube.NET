using MediatR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses.Channel;
using YouTube.Application.DTOs.Channel;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.Channel.GetChannel;

public class GetChannelQueryHandler : IRequestHandler<GetChannelQuery, ChannelResponse>
{
    private readonly IDbContext _context;
    private readonly IS3Service _s3Service;

    public GetChannelQueryHandler(IDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }
    public async Task<ChannelResponse> Handle(GetChannelQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            throw new ValidationException();

        var channel = await _context.Channels
            .AsNoTracking()
            .Include(x => x.BannerImg)
            .Include(x => x.MainImgFile)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (channel is null)
            throw new NotFoundException(ChannelErrorMessage.ChannelNotFound);

        var banner = string.Empty;
        var main = string.Empty;
        
        if (channel.BannerImg is not null)
            banner = await _s3Service.GetFileUrlAsync(channel.BannerImg.BucketName!, channel.BannerImg.FileName,
                cancellationToken);

        if (channel.MainImgFile is not null)
            main = await _s3Service.GetFileUrlAsync(channel.MainImgFile!.BucketName!, channel.MainImgId.ToString()!,
                cancellationToken);
        
        return new ChannelResponse
        {
            IsSuccessfully = true,
            Channel = new ChannelDto
            {
                BannerUrl = banner,
                MainImgUrl = main,
                Subscribers = channel.SubCount,
                Name = channel.Name,
                Description = channel.Description,
                Country = channel.Country,
                CreateDate = channel.CreateDate
            }
        };
    }
}