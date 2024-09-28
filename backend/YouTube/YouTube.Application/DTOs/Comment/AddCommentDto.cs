namespace YouTube.Application.DTOs.Comment;

public class AddCommentDto
{
    public string? CommentText { get; set; }
    
    public int VideoId { get; set; }
    
    public Guid UserId { get; set; }
    
}