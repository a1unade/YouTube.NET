namespace YouTube.Application.Common.Requests.Auth;

public class RefreshTokenRequest
{
    public RefreshTokenRequest()
    {
        
    }

    public RefreshTokenRequest(RefreshTokenRequest request)
    {
        RefreshToken = request.RefreshToken;
    }

    public string RefreshToken { get; set; } = null!;
}