namespace YouTube.Application.DTOs.File;

public class FileIdWithPath
{
    public string Path { get; set; } = default!;
    
    public Guid FileId { get; set; }
}