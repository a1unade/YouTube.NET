namespace YouTube.Application.Common.Requests.User;

public class ChangePasswordRequest
{
    public ChangePasswordRequest()
    {
        
    }

    public ChangePasswordRequest(ChangePasswordRequest request)
    {
        Password = request.Password;
        Id = request.Id;
    }
    
    public string Password { get; set; } = default!;
    
    public Guid Id { get; set; }
}