using YouTube.Application.Common.Responses.VideoResponse;

namespace YouTube.Application.Interfaces;

public interface IVideoService
{
    Task<VideoCommentListResponse> GetVideoCommentList(int id, CancellationToken cancellationToken);
}