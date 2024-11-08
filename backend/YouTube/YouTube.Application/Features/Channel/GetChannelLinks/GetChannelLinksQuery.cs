using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Responses.Channel;

namespace YouTube.Application.Features.Channel.GetChannelLinks;

public class GetChannelLinksQuery : IdRequest, IRequest<ChannelLinksResponse>
{
    public GetChannelLinksQuery(IdRequest request) : base(request)
    {
        
    }
}