namespace YouTube.Application.DTOs.Channel;

public class ChannelDto
{
    public string? BannerUrl { get; set; }

    public string? MainImgUrl { get; set; }
     
    public int Subscribers { get; set; }
    
    public string Name { get; set; } = default!;

    public string? Description { get; set; } 
    
    public string? Country { get; set; }
    
    public DateOnly CreateDate { get; set; }
}