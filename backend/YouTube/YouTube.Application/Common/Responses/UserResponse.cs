using YouTube.Application.Common.Enums;

namespace YouTube.Application.Common.Responses;

public class UserResponse
{
    public UserResponseTypes ResponseType { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string? Susrname { get; set; }
    
    /// <summary>
    /// Пол пользователя
    /// </summary>
    public string? Gender { get; set; }
    
    /// <summary>
    /// Дата рождения пользвователя
    /// </summary>
    public string? BirthDate { get; set; }
    
    /// <summary>
    /// Электронная почта пользователя
    /// </summary>
    public string? Email { get; set; }
    
    public string? Avatar { get; set; }
}