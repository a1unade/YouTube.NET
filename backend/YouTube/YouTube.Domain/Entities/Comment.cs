namespace YouTube.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public string CommentText { get; set; }
    public DateTime PostDate { get; set; }
    public int LikeCount { get; set; }
    public int DisLikeCount { get; set; }
    public int VideoId { get; set; }
    public Video Video { get; set; }
    public Guid UserId { get; set; }
    public UserInfo UserInfo { get; set; }
}