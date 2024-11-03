using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Responses.Chats;

namespace YouTube.Application.Features.Chats.GetChatCollection;

public class GetChatCollectionQuery : PaginationRequest, IRequest<ChatCollectionResponse>
{
    public GetChatCollectionQuery(PaginationRequest request) : base(request)
    {

    }
}