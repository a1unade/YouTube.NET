namespace YouTube.Application.DTOs.Chat;

/// <summary>
/// Сообщение для консьюмера
/// </summary>
public class MessageRequest
{
    /// <summary>
    /// Чат Id
    /// </summary>
    public Guid ChatId { get; set; }
    
    /// <summary>
    /// Юзер Id
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Id сообщения
    /// </summary>
    public Guid MessageId { get; set; }

    /// <summary>
    /// Сообщение
    /// </summary>
    public string Message { get; set; } = default!;
}