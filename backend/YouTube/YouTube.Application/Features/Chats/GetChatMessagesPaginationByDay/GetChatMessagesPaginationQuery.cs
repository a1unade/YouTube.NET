using MediatR;
using YouTube.Application.Common.Requests.Chats;
using YouTube.Application.Common.Responses.Chats;

namespace YouTube.Application.Features.Chats.GetChatMessagesPaginationByDay;

public class GetChatMessagesPaginationQuery : PaginationDaysRequest, IRequest<ChatHistoryResponse>
{
    public GetChatMessagesPaginationQuery(PaginationDaysRequest request) : base(request)
    {
        
    }   
}