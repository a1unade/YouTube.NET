namespace YouTube.Application.Common.Responses.Auth;

public class RefreshTokenResponse : BaseResponse
{
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}