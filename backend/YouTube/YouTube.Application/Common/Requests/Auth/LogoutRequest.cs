namespace YouTube.Application.Common.Requests.Auth;

public class LogoutRequest
{
    public LogoutRequest()
    {
        
    }

    protected LogoutRequest(LogoutRequest request)
    {
        UserId = request.UserId;
    }

    public Guid UserId { get; set; } 
}