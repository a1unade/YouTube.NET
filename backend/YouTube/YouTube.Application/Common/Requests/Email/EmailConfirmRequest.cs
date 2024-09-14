namespace YouTube.Application.Common.Requests.Email;

public class EmailConfirmRequest
{
    public EmailConfirmRequest()
    {
        
    }
    public EmailConfirmRequest(EmailConfirmRequest request)
    {
        Id = request.Id;
        Email = request.Email;
    }
    
    public string Id { get; set; }

    public string Email { get; set; }
}