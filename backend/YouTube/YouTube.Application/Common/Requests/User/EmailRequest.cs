namespace YouTube.Application.Common.Requests.User;

public class EmailRequest
{
    public EmailRequest()
    {
        
    }

    public EmailRequest(EmailRequest request)
    {
        Email = request.Email;
    }
    
    public string Email { get; set; }
}