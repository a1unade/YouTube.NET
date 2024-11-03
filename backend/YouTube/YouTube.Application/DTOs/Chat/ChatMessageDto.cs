namespace YouTube.Application.DTOs.Chat;

/// <summary>
/// Dto для сообщения
/// </summary>
public class ChatMessageDto
{
    /// <summary>
    /// Id отправителя 
    /// </summary>
    public Guid SenderId { get; set; }
    
    /// <summary>
    /// Id сообщения
    /// </summary>
    public Guid MessageId { get; set; }
    
    /// <summary>
    /// Сообщение
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Время отправки
    /// </summary>
    public TimeSpan Time { get; set; } 
    
    /// <summary>
    /// Прочитано ли
    /// </summary>
    public bool IsRead { get; set; }
    
    /// <summary>
    /// Дата
    /// </summary>
    public DateTime Date { get; set; }
}