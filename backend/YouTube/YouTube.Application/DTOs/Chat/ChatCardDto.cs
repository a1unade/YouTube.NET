namespace YouTube.Application.DTOs.Chat;

public class ChatCardDto
{
    public string UserName { get; set; } = default!;
    public string AvatarUrl { get; set; } = default!;
    
    public ChatMessageDto LastMessage { get; set; } = default!;
    
    public Guid ChatId { get; set; }
}