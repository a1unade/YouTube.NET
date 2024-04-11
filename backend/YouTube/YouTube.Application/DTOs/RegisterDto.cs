namespace YouTube.Application.DTOs;

public class RegisterDto
{
    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Никнейм
    /// </summary>
    public string Nickname { get; set; }
}