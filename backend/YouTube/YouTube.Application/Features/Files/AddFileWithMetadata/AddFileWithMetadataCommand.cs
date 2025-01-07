using MediatR;
using YouTube.Application.Common.Requests.Files;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Files.AddFileWithMetadata;

public class AddFileWithMetadataCommand : FileWithMetadataRequest, IRequest<BaseResponse>
{
    public AddFileWithMetadataCommand(FileWithMetadataRequest request) : base(request)
    {
        
    }
}