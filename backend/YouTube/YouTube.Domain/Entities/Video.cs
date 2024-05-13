namespace YouTube.Domain.Entities;

public class Video
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int ViewCount { get; set; }
    public int LikeCount { get; set; }
    
    public int DisLikeCount { get; set; }
    
    public string PathInDisk { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ChannelId { get; set; }
    public Channel Channel { get; set; }
    public List<Comment> Comments { get; set; }
    
    public int PreviewImgId { get; set; }
    public StaticFile StaticFile { get; set; }
}