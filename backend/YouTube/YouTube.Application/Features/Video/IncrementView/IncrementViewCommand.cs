using MediatR;
using YouTube.Application.Common.Requests.Video;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Video.IncrementView;

public class IncrementViewCommand : IncrementViewRequest, IRequest<BaseResponse>
{
    public IncrementViewCommand(IncrementViewRequest request) : base(request)
    {
        
    }
}