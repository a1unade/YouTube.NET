namespace YouTube.Application.DTOs.Auth;

public class UserEmailDto
{
    /// <summary>
    /// Почта пользователя для смены пароля
    /// </summary>
    public required string Email { get; set; }
}