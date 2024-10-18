using MediatR;
using YouTube.Application.Common.Requests.Video;
using YouTube.Application.Common.Responses.Video;

namespace YouTube.Application.Features.Video.GetVideoPagination;

public class GetVideoPaginationQuery : VideoPaginationRequest, IRequest<VideoPaginationResponse>
{
    public GetVideoPaginationQuery(VideoPaginationRequest request) : base(request)
    {
        
    }
}