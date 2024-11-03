namespace YouTube.Application.DTOs.Chat;

/// <summary>
/// Dto для чата
/// </summary>
public class ChatCardDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string UserName { get; set; } = default!;
    
    /// <summary>
    /// Аватар пользователя
    /// </summary>
    public string AvatarUrl { get; set; } = default!;
    
    /// <summary>
    /// Последнее сообщение
    /// </summary>
    public ChatMessageDto LastMessage { get; set; } = default!;
    
    /// <summary>
    /// Id чата
    /// </summary>
    public Guid ChatId { get; set; }
    
    /// <summary>
    /// Дата
    /// </summary>
    public DateTime Date { get; set; }
}