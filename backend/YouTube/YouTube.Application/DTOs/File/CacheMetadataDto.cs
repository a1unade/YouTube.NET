namespace YouTube.Application.DTOs.File;

public class CacheMetadataDto
{
    public MetadataDto Metadata { get; set; } = default!;
    
    public int Counter { get; set; }
}