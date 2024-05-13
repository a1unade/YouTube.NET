
namespace YouTube.Domain.Entities;

public class Channel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime  CreateDate { get; set; }
    public int SubCount { get; set; }
    public Guid UserId { get; set; }
    public UserInfo UserInfo { get; set; }
    public List<Video> Videos { get; set; }
    
    public int? MainImgId { get; set; }
    
    public StaticFile? MainImgFile { get; set; }
    
    public int? BannerImgId { get; set; }
    
    public StaticFile? BannerImgFile { get; set; }
    
    public List<UserChannelSub> UserChannelSubs { get; set; }
    
}