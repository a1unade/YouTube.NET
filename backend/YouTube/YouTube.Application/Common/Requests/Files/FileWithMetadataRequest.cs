using Microsoft.AspNetCore.Http;

namespace YouTube.Application.Common.Requests.Files;

public class FileWithMetadataRequest
{
    public FileWithMetadataRequest()
    {
        
    }

    public FileWithMetadataRequest(FileWithMetadataRequest fileWithMetadataRequest)
    {
        File = fileWithMetadataRequest.File;
        Metadata = fileWithMetadataRequest.Metadata;
        UserId = fileWithMetadataRequest.UserId;
    }
    
    public IFormFile File { get; set; } = default!;

    public Dictionary<string, string> Metadata { get; set; } = default!;
    
    public Guid UserId { get; set; }
}