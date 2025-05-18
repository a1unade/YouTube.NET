using MediatR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Application.Features.Channel.SubscribeToChannel;

public class SubscribeToChannelCommandHandler : IRequestHandler<SubscribeToChannelCommand, BaseResponse>
{
    private readonly IDbContext _context;

    public SubscribeToChannelCommandHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<BaseResponse> Handle(SubscribeToChannelCommand request, CancellationToken cancellationToken)
    {
        var userChannel = await _context.Channels
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (userChannel == null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Channel));
        }

        var subsChannel = await _context.Channels
            .FirstOrDefaultAsync(x => x.Id == request.ChannelToSubsId, cancellationToken);

        if (subsChannel == null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Channel));
        }

        await _context.ChannelSubscriptions.AddAsync(new ChannelSubscription
        {
            Subscriber = userChannel,
            SubscribedTo = subsChannel
        }, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);

        return new BaseResponse
        {
            IsSuccessfully = true,
            Message = "Вы подписались на канал",
            EntityId = userChannel.Id
        };
    }
}