using MediatR;
using YouTube.Application.Common.Requests.Chats;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Chats.PatchReadMessages;

public class PatchReadMessagesCommand : ReadMessagesRequest, IRequest<BaseResponse>
{
    public PatchReadMessagesCommand(ReadMessagesRequest request) : base(request)
    {
        
    }
}