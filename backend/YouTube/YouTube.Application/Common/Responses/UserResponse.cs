using YouTube.Application.Common.Enums;

namespace YouTube.Application.Common.Responses;

public class UserResponse
{
    /// <summary>
    /// Тип ответа
    /// </summary>
    public UserResponseTypes Type { get; set; }

    /// <summary>
    /// Описание ответа
    /// </summary>
    public string Message { get; set; } = "";
}