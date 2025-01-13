using Microsoft.AspNetCore.Http;

namespace YouTube.Application.Common.Requests.Files;

public class MetadataRequest
{
    public MetadataRequest()
    {
        
    }

    public MetadataRequest(MetadataRequest metadataRequest)
    {
        FileName = metadataRequest.FileName;
        Size = metadataRequest.Size;
        ContentType = metadataRequest.ContentType;
        UserId = metadataRequest.UserId;
    }

    public string FileName { get; set; } = default!;
    
    public long Size { get; set; }

    public string ContentType { get; set; } = default!;
    
    public Guid UserId { get; set; }
}