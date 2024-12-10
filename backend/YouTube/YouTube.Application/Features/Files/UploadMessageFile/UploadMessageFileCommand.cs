using MediatR;
using YouTube.Application.Common.Requests.Chats;
using YouTube.Application.Common.Responses;

namespace YouTube.Application.Features.Files.UploadMessageFile;

public class UploadMessageFileCommand : UploadMessageFileRequest, IRequest<BaseResponse>
{
    public UploadMessageFileCommand(UploadMessageFileRequest request) : base(request)
    {
        
    }
}