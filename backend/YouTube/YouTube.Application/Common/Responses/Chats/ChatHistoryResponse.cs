using YouTube.Application.DTOs.Chat;

namespace YouTube.Application.Common.Responses.Chats;

/// <summary>
/// Ответ истории чата
/// </summary>
public class ChatHistoryResponse : BaseResponse
{
    /// <summary>
    /// Список сообщений
    /// </summary>
    public List<ChatMessageDto>? ChatMessages { get; set; }
    
    /// <summary>
    /// Страница
    /// </summary>
    public int PageCount { get; set; }
}