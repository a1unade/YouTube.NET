using YouTube.Application.DTOs.Chat;

namespace YouTube.Application.Common.Responses.Chats;

public class ChatCardResponse : BaseResponse
{
    public List<ChatCardDto> ChatCardDtos { get; set; } = default!;
    
    public int PageCount { get; set; }
}