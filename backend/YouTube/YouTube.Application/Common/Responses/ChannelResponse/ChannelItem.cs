namespace YouTube.Application.Common.Responses.ChannelResponse;

public class ChannelItem
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public int Subscription { get; set; }
    
    public required string Description { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public required string MainImg { get; set; }
    
    public required string BannerImg { get; set; }
    
    public int VideoCount { get; set; }
    
    public int ViewCount { get; set; }
    
    public required string Country { get; set; }
}