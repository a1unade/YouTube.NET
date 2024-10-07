using MediatR;
using YouTube.Application.Common.Requests.Video;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Video.UploadVideo;

public class UploadVideoCommand : UploadVideoRequest, IRequest<BaseResponse>
{
    public UploadVideoCommand(UploadVideoRequest request) : base(request)
    {
        
    }
}