namespace YouTube.Application.Common.Responses.CommentResponse;

public class CommentItem
{
    public int Id { get; set; }
    
    public required string CommentText { get; set; }
    
    public DateTime PostDate { get; set; }
    
    public int LikeCount { get; set; }
    
    public int DisLikeCount { get; set; }
    
    public required string UserName { get; set; }
}