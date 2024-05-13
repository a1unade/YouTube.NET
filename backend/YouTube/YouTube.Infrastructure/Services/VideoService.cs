using YouTube.Application.Common.Responses.CommentResponse;
using YouTube.Application.Common.Responses.VideoResponse;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Infrastructure.Services;

public class VideoService : IVideoService
{
    private readonly ICommentRepository _commentRepository;

    public VideoService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<VideoCommentListResponse> GetVideoCommentList(int id, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetVideoComment(id, cancellationToken);

        if (comments.Count == 0)
            return new VideoCommentListResponse()
            {
                IsSuccessfully = true,
                Message = "0 Comments"
            };

        var result = new List<CommentItem>();

        foreach (var comment in comments)
        {
            result.Add(new CommentItem()
            {
                CommentText = comment.CommentText,
                Id = comment.Id,
                DisLikeCount = comment.DisLikeCount,
                LikeCount = comment.LikeCount,
                PostDate = comment.PostDate,
                UserName = comment.UserInfo.Name
            });
        }

        return new VideoCommentListResponse()
        {
            IsSuccessfully = true,
            Comments = result
        };
    }
}