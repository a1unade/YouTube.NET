using YouTube.Application.Common.Responses;
using YouTube.Application.Common.Responses.VideoResponse;
using YouTube.Application.DTOs.Comment;

namespace YouTube.Application.Interfaces;

public interface IVideoService
{
    Task<BaseResponse> AddComment(AddCommentDto request, CancellationToken cancellationToken);
    Task<VideoCommentListResponse> GetVideoCommentList(int id, CancellationToken cancellationToken);
    Task<VideoListResponse> GetRandomVideo(CancellationToken cancellationToken);
}