
namespace YouTube.Application.Common.Responses.Auth;

/// <summary>
/// Ответ при регистрации
/// </summary>
public class AuthResponse : BaseResponse
{
    
    /// <summary>
    /// ID пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Jwt токен
    /// </summary>
    public string Token { get; set; } = null!;
    
    /// <summary>
    /// Рефреш токен
    /// </summary>
    public string RefreshToken { get; set; } = null!;
}