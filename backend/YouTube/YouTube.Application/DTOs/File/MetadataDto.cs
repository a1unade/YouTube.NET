namespace YouTube.Application.DTOs.File;

public class MetadataDto
{
    public string FileName { get; set; } = default!;
    
    public long Size { get; set; }

    public string ContentType { get; set; } = default!;

    public Guid FileId { get; set; }
    
    public Guid UserId { get; set; }
}