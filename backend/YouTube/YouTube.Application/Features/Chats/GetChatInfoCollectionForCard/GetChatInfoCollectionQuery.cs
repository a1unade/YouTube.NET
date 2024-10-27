using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Responses.Chats;

namespace YouTube.Application.Features.Chats.GetChatInfoCollectionForCard;

public class GetChatInfoCollectionQuery : PaginationRequest, IRequest<ChatCardResponse>
{
    public GetChatInfoCollectionQuery(PaginationRequest request) : base(request)
    {
        
    }
}