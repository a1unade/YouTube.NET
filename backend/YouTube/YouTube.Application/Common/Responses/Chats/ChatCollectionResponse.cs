namespace YouTube.Application.Common.Responses.Chats;

public class ChatCollectionResponse : BaseResponse
{
    public List<Guid> ChatsId { get; set; } = default!;
}