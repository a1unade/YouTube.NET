using YouTube.Application.DTOs.Chat;

namespace YouTube.Application.Common.Responses.Chats;

public class ChatHistoryResponse : BaseResponse
{
    public List<ChatMessageDto> ChatMessages { get; set; } = default!;
}