using MediatR;
using YouTube.Application.Common.Responses.Chats;

namespace YouTube.Application.Features.Chats.GetChatCollection;

public class GetChatCollectionQueryHandler : IRequestHandler<GetChatCollectionQuery, ChatCollectionResponse>
{
    public GetChatCollectionQueryHandler()
    {
        
    }
    public Task<ChatCollectionResponse> Handle(GetChatCollectionQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}