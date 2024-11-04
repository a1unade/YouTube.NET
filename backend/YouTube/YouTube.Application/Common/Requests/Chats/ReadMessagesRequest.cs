namespace YouTube.Application.Common.Requests.Chats;

public class ReadMessagesRequest
{
    public Guid ChatId { get; set; }
    public List<Guid> MessagesId { get; set; } = default!;
}