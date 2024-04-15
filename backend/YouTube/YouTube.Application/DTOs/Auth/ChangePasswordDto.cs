namespace YouTube.Application.DTOs.Auth;

public class ChangePasswordDto
{
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Новый пароль для смены старого
    /// </summary>
    public string NewPassword { get; set; }
}