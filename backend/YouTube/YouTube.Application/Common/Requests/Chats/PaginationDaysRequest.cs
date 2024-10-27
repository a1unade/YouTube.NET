namespace YouTube.Application.Common.Requests.Chats;

public class PaginationDaysRequest
{
    public PaginationDaysRequest()
    {
        
    }

    public PaginationDaysRequest(PaginationDaysRequest request)
    {
        Page = request.Page;
        ChatId = request.ChatId;
    }
    public int Page { get; set; }
    
    public Guid ChatId { get; set; }
}