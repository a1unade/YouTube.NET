using MediatR;
using YouTube.Application.Common.Requests.Files;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Files.UploadFile;

public class UploadFileCommand : FileRequest, IRequest<BaseResponse>
{
    public UploadFileCommand(FileRequest request) : base(request)
    {
        
    }
}