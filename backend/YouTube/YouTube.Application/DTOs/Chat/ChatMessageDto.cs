using YouTube.Application.DTOs.File;

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
    /// Файл
    /// </summary>
    public FileTypeDto? Attachment { get; set; }

    /// <summary>
    /// Время отправки
    /// </summary>
    public TimeOnly Time { get; set; } 
    
    /// <summary>
    /// Прочитано ли
    /// </summary>
    public bool IsRead { get; set; }
    
    /// <summary>
    /// Дата
    /// </summary>
    public DateOnly Date { get; set; }
}