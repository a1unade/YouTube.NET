using YouTube.Application.Common.Enums;

namespace YouTube.Application.Common.Responses;

public class AuthResponse
{
    /// <summary>
    /// Тип ответа
    /// </summary>
    public UserResponseTypes Type { get; set; }

    /// <summary>
    /// Описание ответа
    /// </summary>
    public string Message { get; set; } = "";
    
    /// <summary>
    /// ID пользователя
    /// </summary>
    public string? UserId { get; set; }
}