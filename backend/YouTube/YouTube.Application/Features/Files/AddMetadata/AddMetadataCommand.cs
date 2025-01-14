using MediatR;
using YouTube.Application.Common.Requests.Files;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Files.AddMetadata;

public class AddMetadataCommand : MetadataRequest, IRequest<BaseResponse>
{
    public AddMetadataCommand(MetadataRequest request) : base(request)
    {
        
    }
}