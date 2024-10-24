using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Responses.Chats;

namespace YouTube.Application.Features.Chats.GetChatHistory;

public class GetChatHistoryQuery : IdRequest, IRequest<ChatHistoryResponse>
{
    public GetChatHistoryQuery(IdRequest request) : base(request)
    {
        
    }
}