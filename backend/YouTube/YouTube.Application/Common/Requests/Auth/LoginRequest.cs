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

    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;


}