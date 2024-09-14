namespace YouTube.Application.Common.Requests.User;

public class ChangePasswordRequest
{
    public ChangePasswordRequest()
    {
        
    }

    public ChangePasswordRequest(ChangePasswordRequest request)
    {
        Password = request.Password;
        Email = request.Email;
    }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
}