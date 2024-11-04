using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;
/// <summary>
/// Сообщение с техподдержки
/// </summary>
public class ChatMessage : BaseEntity
{
    /// <summary>
    /// Сообщение
    /// </summary>
    public string? Message { get; set; } 
    
    /// <summary>
    /// Время
    /// </summary>
    public TimeOnly Time { get; set; }
    
    /// <summary>
    /// Дата
    /// </summary>
    public DateOnly Date { get; set; }
    
    /// <summary>
    /// Прочитано?
    /// </summary>
    public bool IsRead { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Пользователь 
    /// </summary>
    public User User { get; set; } = default!;
    
    /// <summary>
    /// Id сессии чата
    /// </summary>
    public Guid ChatHistoryId { get; set; }

    /// <summary>
    /// Сессия чата
    /// </summary>
    public ChatHistory ChatHistory { get; set; } = default!;
    
    /// <summary>
    /// Ид файла
    /// </summary>
    public Guid? FileId { get; set; }
    
    /// <summary>
    /// Файл
    /// </summary>
    public File? File { get; set; }
}