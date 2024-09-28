namespace YouTube.Application.DTOs.Auth;

public class RegisterDto
{
    /// <summary>
    /// Электронная почта
    /// </summary>
    public required string Email { get; set; } 

    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; set; }
    
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
    public required string Gender { get; set; }
}