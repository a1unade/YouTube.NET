namespace YouTube.Application.DTOs.Chat;

public class ChatMessageDto
{
    public string? FileUrl { get; set; }
    
    public string? Message { get; set; }
    
    public DateTime Time { get; set; }
}