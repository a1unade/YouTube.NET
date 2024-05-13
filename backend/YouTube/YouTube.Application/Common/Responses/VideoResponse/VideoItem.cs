namespace YouTube.Application.Common.Responses.VideoResponse;

public class VideoItem
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public int ViewCount { get; set; }
    
    public int LikeCount { get; set; }
    
    public int DisLikeCount { get; set; }

    public DateTime ReleaseDate { get; set; }

    public string? PreviewImg { get; set; }
}