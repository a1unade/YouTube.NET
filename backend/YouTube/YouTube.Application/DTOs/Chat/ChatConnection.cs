namespace YouTube.Application.DTOs.Chat;
/// <summary>
/// Подключение к чату
/// </summary>
public class ChatConnection
{
    /// <summary>
    /// Id Пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Id Чата
    /// </summary>
    public Guid? ChatId { get; set; }
}