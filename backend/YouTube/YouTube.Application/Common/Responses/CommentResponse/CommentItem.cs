namespace YouTube.Application.Common.Responses.CommentResponse;

public class CommentItem
{
    public int Id { get; set; }
    public string CommentText { get; set; }
    public DateTime PostDate { get; set; }
    public int LikeCount { get; set; }
    public int DisLikeCount { get; set; }
    public string UserName { get; set; }
}