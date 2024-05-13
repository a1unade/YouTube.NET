namespace YouTube.Application.Common.Responses.ChannelResponse;

public class ChannelItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int Subscription { get; set; }
    
    public string Description { get; set; }
    
    public DateTime Createdate { get; set; }
    
    public string MainImg { get; set; }
    
    public string BannerImg { get; set; }
    
    public int VideoCount { get; set; }
    
    public int ViewCount { get; set; }
    
    public string Country { get; set; }
}