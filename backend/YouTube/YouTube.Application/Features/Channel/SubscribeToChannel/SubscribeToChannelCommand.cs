using MediatR;
using YouTube.Application.Common.Requests.Channel;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Channel.SubscribeToChannel;

public class SubscribeToChannelCommand : SubscribeToChannelRequest, IRequest<BaseResponse>
{
    public SubscribeToChannelCommand(SubscribeToChannelRequest request) : base(request)
    {
        
    }
}