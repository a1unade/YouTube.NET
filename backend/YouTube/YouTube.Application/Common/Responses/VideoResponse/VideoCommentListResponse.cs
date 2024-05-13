using YouTube.Application.Common.Responses.CommentResponse;

namespace YouTube.Application.Common.Responses.VideoResponse;

public class VideoCommentListResponse : BaseResponse
{
    public List<CommentItem> Comments { get; set; }
}