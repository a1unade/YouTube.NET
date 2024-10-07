using Microsoft.AspNetCore.Http;

namespace YouTube.Application.Common.Requests.Video;

public class UploadVideoRequest
{
    public UploadVideoRequest()
    {
        
    }

    public UploadVideoRequest(UploadVideoRequest request)
    {
        Files = request.Files;
        ChannelId = request.ChannelId;
        Name = request.Name;
        Description = request.Description;
        Category = request.Category;
        IsHidden = request.IsHidden;
    }

    public List<IFormFile> Files { get; set; } = default!;
    
    public Guid ChannelId { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string Category { get; set; } = default!;
    
    public bool IsHidden { get; set; }
}