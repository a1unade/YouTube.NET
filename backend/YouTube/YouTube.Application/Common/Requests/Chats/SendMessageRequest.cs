namespace YouTube.Application.Common.Requests.Chats;
/// <summary>
/// Сообщение
/// </summary>
public class SendMessageRequest
{
    /// <summary>
    /// Id отправителя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Id чата
    /// </summary>
    public Guid ChatId { get; set; }
    
    /// <summary>
    /// Сообщение 
    /// </summary>
    public string Message { get; set; } = default!;
}