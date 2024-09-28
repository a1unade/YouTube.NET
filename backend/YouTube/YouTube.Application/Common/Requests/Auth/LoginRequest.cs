namespace YouTube.Application.Common.Requests.Auth;

public class LoginRequest
{
    public LoginRequest()
    {
    }

    public LoginRequest(LoginRequest request)
    {
        Email = request.Email;
        Password = request.Password;
    }
    
    public string? Email { get; set; }
    
    public string? Password { get; set; }
    
    
}