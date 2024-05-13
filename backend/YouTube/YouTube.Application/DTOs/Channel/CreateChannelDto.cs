namespace YouTube.Application.DTOs.Channel;

public class CreateChannelDto
{
    public Guid UserId { get; set; }
    
    public string ChannelName { get; set; }
}