namespace YouTube.Application.Common.Requests.Chats;

public class ReadMessagesRequest
{
    public ReadMessagesRequest()
    {
        
    }

    public ReadMessagesRequest(ReadMessagesRequest request)
    {
        MessagesId = request.MessagesId;
    }
    
    public List<Guid> MessagesId { get; set; } = default!;
}