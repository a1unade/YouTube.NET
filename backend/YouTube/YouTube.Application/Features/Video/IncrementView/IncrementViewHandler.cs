using MassTransit;
using MediatR;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Requests.Video;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.Video.IncrementView;

public class IncrementViewHandler(IClickHouseService clickHouseService, IBus bus) : IRequestHandler<IncrementViewCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(IncrementViewCommand request, CancellationToken cancellationToken)
    {
        if (request.VideoId == Guid.Empty)
        {
            throw new ValidationException("VideoId cannot be empty.");
        }
        
        var view = await clickHouseService.IncrementView(request.VideoId, cancellationToken);
        var message = new SendIncrementViewMessage(view.Id, view.VideoId, view.ViewCount);
        
        await bus.Publish(message, cancellationToken);
            
        return new BaseResponse(true, "View incremented and event published.");
    }
}