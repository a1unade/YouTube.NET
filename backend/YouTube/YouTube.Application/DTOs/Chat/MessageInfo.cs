using YouTube.Application.DTOs.Video;

namespace YouTube.Application.DTOs.Chat;

public class MessageInfo
{
    public Guid UserId { get; set; }
    
    public string? Message { get; set; }

    public FileContent? FileContent { get; set; }
}