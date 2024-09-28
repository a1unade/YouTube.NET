namespace YouTube.Application.DTOs.Auth;

public class ChangePasswordDto
{
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Новый пароль для смены старого
    /// </summary>
    public required string NewPassword { get; set; }
}