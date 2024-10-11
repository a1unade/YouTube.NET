using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Responses.Channel;

namespace YouTube.Application.Features.Channel.GetChannel;

public class GetChannelQuery : IdRequest, IRequest<ChannelResponse>
{
    public GetChannelQuery(IdRequest request) : base(request)
    {
        
    }
}