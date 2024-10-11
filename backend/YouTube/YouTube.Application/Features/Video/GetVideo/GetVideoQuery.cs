using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Responses.Video;

namespace YouTube.Application.Features.Video.GetVideo;

public class GetVideoQuery : IdRequest, IRequest<VideoResponse>
{
    public GetVideoQuery(IdRequest request) : base(request)
    {
        
    }
}