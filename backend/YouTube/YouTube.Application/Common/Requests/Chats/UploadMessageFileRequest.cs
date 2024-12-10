using Microsoft.AspNetCore.Http;

namespace YouTube.Application.Common.Requests.Chats;

public class UploadMessageFileRequest
{
    public UploadMessageFileRequest()
    {
        
    }

    public UploadMessageFileRequest(UploadMessageFileRequest request)
    {
        UserId = request.UserId;
        File = request.File;
    }
    public Guid UserId { get; set; }
    
    public IFormFile File { get; set; } = default!;
}