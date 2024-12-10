using Microsoft.AspNetCore.Http;

namespace YouTube.Application.DTOs.File;

public class FileFromChatDto
{
    public Guid MessageId { get; set; }

    public IFormFile File { get; set; } = default!;
}