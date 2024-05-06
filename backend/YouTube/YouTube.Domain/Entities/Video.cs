namespace YouTube.Domain.Entities;

public class Video
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int ViewCount { get; set; }
    
    public int LikeCount { get; set; }
    
    public int DisLikeCount { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    public int CommentCount { get; set; }
    
    public Guid UserId { get; set; }
     
    public User User { get; set; }
}