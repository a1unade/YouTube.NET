namespace YouTube.Application.DTOs.File;


public class FileTypeDto
{
    public Guid? FileId { get; set; }

    public string ContentType { get; set; } = default!;
}