namespace YouTube.Application.DTOs.Channel;

public class ChannelDto
{
    public Guid Id { get; set; }
    public string? BannerImage { get; set; }

    public string? MainImage { get; set; }
     
    public int Subscribers { get; set; }
    
    public string Name { get; set; } = default!;

    public string? Description { get; set; } 
    
    public int VideoCount { get; set; }
}