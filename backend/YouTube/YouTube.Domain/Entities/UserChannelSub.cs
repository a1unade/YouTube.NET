namespace YouTube.Domain.Entities;

public class UserChannelSub
{
    public Guid UserId { get; set; }
    
    public int ChannelId { get; set; }
    
    public UserInfo UserInfo { get; set; }
    
    public Channel Channel { get; set; }
}