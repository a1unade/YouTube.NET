namespace YouTube.Application.DTOs.Auth;

public class RegisterDto
{
    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = "";

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; } = "";
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string? Surname { get; set; }
    
    /// <summary>
    /// Дата рождения пользователя
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Пол пользователя
    /// </summary>
    public string Gender { get; set; } = "";
}