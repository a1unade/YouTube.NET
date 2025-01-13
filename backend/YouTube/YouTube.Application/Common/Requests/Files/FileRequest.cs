using Microsoft.AspNetCore.Http;

namespace YouTube.Application.Common.Requests.Files;

public class FileRequest
{
    public FileRequest()
    {
        
    }

    public FileRequest(FileRequest request)
    {
        File = request.File;
        FileId = request.FileId;
        UserId = request.UserId;
    }
    
    
    public IFormFile File { get; set; } = default!;
    
    public Guid FileId { get; set; }
    
    public Guid UserId { get; set; }
}