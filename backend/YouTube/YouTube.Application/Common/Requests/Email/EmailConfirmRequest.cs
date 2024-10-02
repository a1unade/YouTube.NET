namespace YouTube.Application.Common.Requests.Email;

public class EmailConfirmRequest 
{
    public EmailConfirmRequest()
    {
        
    }
    public EmailConfirmRequest(EmailConfirmRequest request)
    {
        Id = request.Id;
    }
    
    public Guid Id { get; set; } 
}